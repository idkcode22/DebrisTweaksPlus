using System.Collections.Generic;
using System.Reflection;
using DebrisTweaks;
using HarmonyLib;
using IPA.Config.Data;
using IPA.Utilities;
using JetBrains.Annotations;
using ModestTree;
using UnityEngine;
[HarmonyPatch(typeof(NoteDebrisSpawner))]
public static class NoteDebrisSpawnerPatch
{
    private static readonly MethodInfo SpawnNoteDebrisMethod = AccessTools.Method(typeof(NoteDebrisSpawner), "SpawnNoteDebris");

    [HarmonyPrefix]
    [HarmonyPatch(nameof(NoteDebrisSpawner.SpawnDebris))]
    public static bool SpawnDebrisPrefix(
        NoteDebrisSpawner __instance,
        NoteData.GameplayType noteGameplayType,
        Vector3 cutPoint,
        Vector3 cutNormal,
        float saberSpeed,
        Vector3 saberDir,
        Vector3 notePos,
        Quaternion noteRotation,
        Vector3 noteScale,
        ColorType colorType,
        float timeToNextColorNote,
        Vector3 moveVec)
    {
        Config config = Config.Instance;
        if (!config.ModToggle) return true;
        // Default constants
        float kMinLifeTime = config.minLifetime;
        float kMaxLifeTime = config.maxLifetime;
        float kLifeTimeOffset = config.lifeTimeOffset;

        float rotation = config.rotation;
        float cutDirMultiplier = config.cutDirMultiplier * 0.1f; //0.025
        float fromCenterSpeed = config.fromCenterSpeed;
        float moveSpeedMultiplier = config.moveSpeedMultiplier;

        float randomRotation = config.randomRotation;

        Vector3 force = Vector3.zero;
        Vector3 force2 = Vector3.zero;
        Vector3 torque = Vector3.zero;

        // Use reflection to call the private SpawnNoteDebris method
        object[] spawnNoteDebrisParams = { noteGameplayType, null, null }; // Output parameters for debris objects
        SpawnNoteDebrisMethod.Invoke(__instance, spawnNoteDebrisParams);

        // Retrieve the spawned debris
        var debris = spawnNoteDebrisParams[1] as NoteDebris;
        var debris2 = spawnNoteDebrisParams[2] as NoteDebris;
        if (debris == null && debris2 == null) return false;

        // Initialize debris objects
        debris.didFinishEvent.Add(__instance);
        debris.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        debris2.didFinishEvent.Add(__instance);
        debris2.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        // Calculate parameters
        float magnitude = moveVec.magnitude;
        float lifeTime = Mathf.Clamp(timeToNextColorNote + kLifeTimeOffset, kMinLifeTime, kMaxLifeTime) * config.lifeTimeMultiplier;
        Vector3 vector = Vector3.ProjectOnPlane(saberDir, moveVec / magnitude);
        Vector3 vector2 = vector * (saberSpeed * cutDirMultiplier) + (moveVec * moveSpeedMultiplier);


        noteScale *= config.DebrisScale;
        // Adjust vertical force based on cut height
        if (config.adjustVerticalForceToggle)
        {
            if (cutPoint.y < 1.3f)
            {
                vector2.y = Mathf.Min(vector2.y, 0f);
            }
            else if (cutPoint.y > 1.3f)
            {
                vector2.y = Mathf.Max(vector2.y, 0f);
            }
        }
        // if random rotation then use config value if not then use 0f
        randomRotation = config.randomRotationToggle ? config.randomRotation : 0f;
        //determind what the minium saberspeed is
        float dynamicSaberSpeed = config.dynamicDebrisToggle ? saberSpeed * config.saberSens/10 : 1f;
        if (config.dynamicDebrisToggle && (dynamicSaberSpeed < magnitude * 0.03f))
        {
            dynamicSaberSpeed = magnitude * 0.03f;
        }
        //calulate forces and rotation
        Quaternion debrisRotation = __instance.transform.rotation;
        // Transform rotation
        force = debrisRotation * (-(cutNormal + Random.onUnitSphere * config.randomCutFromCenter) * (dynamicSaberSpeed * fromCenterSpeed) + vector2);
        force2 = debrisRotation * ((cutNormal + Random.onUnitSphere * config.randomCutFromCenter) * (dynamicSaberSpeed * fromCenterSpeed) + vector2);
        torque = debrisRotation * (Random.insideUnitSphere * randomRotation * (dynamicSaberSpeed)) + Vector3.Cross(cutNormal, vector) * (rotation * (dynamicSaberSpeed));
        // Set debris position and rotation
        Quaternion rotations = __instance.transform.rotation;
        force *= config.forceMultiplier;
        force2 *= config.forceMultiplier;
        Vector3 position = __instance.transform.position;
        Vector3 cutoutOffset = Random.insideUnitSphere;
        Vector3 cutoutOffset2 = Random.insideUnitSphere;

        // compute spawn-relative, world-aligned offset for X (lateral), Y (vertical) and Z (backwards)
        float eps = 1e-6f;
        Vector3 forward = magnitude > eps ? (moveVec / magnitude) : (rotations * Vector3.forward);
        Vector3 right = Vector3.Cross(Vector3.up, forward);
        if (right.sqrMagnitude < eps)
        {
            // fallback if forward is parallel to world up
            right = rotations * Vector3.right;
        }
        else
        {
            right.Normalize();
        }
        Vector3 up = Vector3.up;
        Vector3 offsetPositioning = right * config.debrisOffsetX*-1 + up * config.debrisOffsetY + forward * (-config.debrisOffsetZ);

        debris.Init(colorType, notePos, noteRotation, moveVec, noteScale, position + offsetPositioning, rotations, cutPoint, -cutNormal, force, -torque, lifeTime, cutoutOffset, false);
        debris2.Init(colorType, notePos, noteRotation, moveVec, noteScale, position + offsetPositioning, rotations, cutPoint, cutNormal, force2, torque, lifeTime, cutoutOffset2, false);
        return false; // Skip the original method
    }

    [HarmonyPatch(typeof(NoteDebris), nameof(NoteDebris.Init))]
    internal class NoteDebris_Init
    {
        [HarmonyPostfix]
        public static void Postfix(NoteDebris __instance, ColorType colorType, float ____lifeTime, MaterialPropertyBlockController ____materialPropertyBlockController, int ____colorID)
        {
            Config config = Config.Instance;
            if (!config.ModToggle) return;

            Rigidbody rb = __instance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float randomdrag = Random.Range(config.DragMin, config.DragMax);
                float drag = config.RandomDrag ? randomdrag : config.Drag;
                rb.freezeRotation = config.RotationToggle;
                rb.drag = drag;
                rb.useGravity = config.GravityToggle;
            }

            // Only touch material if the injected field was found and custom colors are enabled. Prevents multiplayer debris spawning issues.
            if (____materialPropertyBlockController != null && config.CustomColourToggle)
            {
                try
                {
                    if (colorType == ColorType.ColorA)
                        ____materialPropertyBlockController.materialPropertyBlock.SetColor(____colorID, config.LeftColour);
                    else if (colorType == ColorType.ColorB)
                        ____materialPropertyBlockController.materialPropertyBlock.SetColor(____colorID, config.RightColour);

                    ____materialPropertyBlockController.ApplyChanges();
                }
                catch
                {
                    // Defensive: don't let any material errors break debris (multiplayer remote objects differ).
                }
            }

            // Disable dissolve animation by setting cutout curve to flat 0
            if (config.disableDissolveAnim)
            {
                try
                {
                    var cutoutCurveField = AccessTools.Field(typeof(NoteDebris), "_cutoutCurve");
                    var cutoutCurveTextureOffsetIDField = AccessTools.Field(typeof(NoteDebris), "_cutoutTexOffsetID");
                    if (cutoutCurveField != null)
                    {
                        // Curve that always evaluates to 0 across [0,1]
                        AnimationCurve flatZeroCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);
                        cutoutCurveField.SetValue(__instance, flatZeroCurve);
                    }
                }
                catch
                {
                    // Defensive: don't let reflection errors break debris.
                }
            }

            // Override dissolve noise scale
            if (config.overrideDissolveNoiseScale && !config.disableDissolveAnim)
            {
                try
                {
                    ____materialPropertyBlockController.materialPropertyBlock.SetFloat("_CutoutTexScale", config.dissolveNoise);
                }
                catch
                {
                    //better safe than sorry
                }
            }


            // Downscale over lifetime component
            if (config.downscaleDespawnAnim)
            {

                try
                {
                    var existing = __instance.GetComponent<DebrisScaleOverLifetime>();
                    if (existing != null)
                    {
                        existing.Initialize(____lifeTime, __instance.transform.localScale);
                    }
                    else
                    {
                        var scaler = __instance.gameObject.AddComponent<DebrisScaleOverLifetime>();
                        scaler.Initialize(____lifeTime, __instance.transform.localScale);
                    }

                }
                catch
                {
                    // Defensive: ignore component errors.
                }
            }
        }
    }

    // Helper component that scales the debris from its initial scale to zero over the configured final seconds.
    // It holds the original size until half of the lifespan is passed after spawn, then scales to zero over the remaining lifetime.
    // Attached in Init postfix so it doesn't require patching NoteDebris.Update.
    internal class DebrisScaleOverLifetime : MonoBehaviour
    {
        private float _lifeTime = 1f;
        private float _elapsed = 0f;
        private Vector3 _initialScale = Vector3.one;
        private float _scaleStartTime = 0f;

        public void Initialize(float lifeTime, Vector3 initialScale)
        {
            _lifeTime = lifeTime;
            _initialScale = initialScale;

            // if holdSeconds >= lifeTime, scaling will happen over whole lifetime (start at t=0)
            _scaleStartTime = Mathf.Max(0f, _lifeTime - lifeTime / 2); //debris stays orginal size for half its lifetime.
            _elapsed = 0f;

            // Ensure starting scale
            transform.localScale = _initialScale;
        }

        void Update()
        {
            if (_lifeTime <= 0f) return;
            _elapsed += Time.deltaTime;

            if (_elapsed < _scaleStartTime)
            {
                // Hold original scale until scale start time
                transform.localScale = _initialScale;
                return;
            }

            float scaleDuration = Mathf.Max(0.0001f, _lifeTime - _scaleStartTime);
            float t = Mathf.Clamp01((_elapsed - _scaleStartTime) / scaleDuration);
            transform.localScale = Vector3.Lerp(_initialScale, Vector3.zero, t);

            // self-clean when finished so pooled objects don't keep stale components
            if (t >= 1f)
            {
                Destroy(this);
            }
        }
    }
}