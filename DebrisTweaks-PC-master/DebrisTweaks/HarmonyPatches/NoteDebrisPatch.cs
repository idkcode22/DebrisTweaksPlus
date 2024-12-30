using HarmonyLib;
using UnityEngine;
using System.Reflection;
using System.Windows.Forms;
using DebrisTweaks;
using JetBrains.Annotations;
[HarmonyPatch(typeof(NoteDebrisSpawner))]
public static class NoteDebrisSpawnerPatch
{
    private static readonly FieldInfo RotationField = AccessTools.Field(typeof(NoteDebrisSpawner), "_rotation");
    private static readonly FieldInfo CutDirMultiplierField = AccessTools.Field(typeof(NoteDebrisSpawner), "_cutDirMultiplier");
    private static readonly FieldInfo FromCenterSpeedField = AccessTools.Field(typeof(NoteDebrisSpawner), "_fromCenterSpeed");
    private static readonly FieldInfo MoveSpeedMultiplierField = AccessTools.Field(typeof(NoteDebrisSpawner), "_moveSpeedMultiplier");

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
        float cutDirMultiplier = config.cutDirMultiplier*0.1f; //0.025
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
        float lifeTime = Mathf.Clamp(timeToNextColorNote + kLifeTimeOffset, kMinLifeTime, kMaxLifeTime);
        Vector3 vector = Vector3.ProjectOnPlane(saberDir, moveVec / magnitude);
        Vector3 vector2 = vector * (saberSpeed * cutDirMultiplier) + (moveVec * moveSpeedMultiplier);

        if (config.DebrisScale != 1f)
        {
            noteScale = Vector3.one * config.DebrisScale;
        }
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


        if (config.randomRotationToggle)
        {
            randomRotation = config.randomRotation;
        }
        else
        {
            randomRotation = 0f;
        }
        //determind what the minium saberspeed is
        float dynamicSaberSpeed = saberSpeed/config.saberSens;
        if (dynamicSaberSpeed < magnitude * 0.03f)
        {
            dynamicSaberSpeed = magnitude * 0.03f;
        }
        //calulate forces and rotation
        Quaternion debrisRotation = __instance.transform.rotation;
        if (config.dynamicDebrisToggle)
        {
            // Transform rotation
            force = debrisRotation * (-(cutNormal + Random.onUnitSphere * config.randomCutFromCenter) * (dynamicSaberSpeed * fromCenterSpeed) + vector2);
            force2 = debrisRotation * ((cutNormal + Random.onUnitSphere * config.randomCutFromCenter) * (dynamicSaberSpeed * fromCenterSpeed) + vector2);
            torque = debrisRotation * (Random.insideUnitSphere * randomRotation * (dynamicSaberSpeed)) + Vector3.Cross(cutNormal, vector) * (rotation * (dynamicSaberSpeed));
        }
        else
        {
            // Transform rotation
            force = debrisRotation * (-(cutNormal + Random.onUnitSphere * config.randomCutFromCenter) * fromCenterSpeed + vector2);
            force2 = debrisRotation * ((cutNormal + Random.onUnitSphere * config.randomCutFromCenter) * fromCenterSpeed + vector2);
            torque = debrisRotation * (Random.insideUnitSphere * randomRotation) + (Vector3.Cross(cutNormal, vector) * rotation);
        }
        // Set debris position and rotation
        force *= config.forceMultiplier;
        force2 *= config.forceMultiplier;
        Vector3 position = __instance.transform.position;
        debris.Init(colorType, notePos, noteRotation, moveVec, noteScale, position, debrisRotation, cutPoint, -cutNormal, force, -torque, lifeTime);
        debris2.Init(colorType, notePos, noteRotation, moveVec, noteScale, position, debrisRotation, cutPoint, cutNormal, force2, torque, lifeTime);

        return false; // Skip the original method
    }

    [HarmonyPatch(typeof(NoteDebris), nameof(NoteDebris.Init))]
    internal class NoteDebris_Init
    {
        [HarmonyPostfix]
        public static void Postfix(NoteDebris __instance, ref ColorType colorType, ref float ____lifeTime, ref MaterialPropertyBlockController ____materialPropertyBlockController, ref int ____colorID)
        {
            Config config = Config.Instance;
            if (!config.ModToggle || !config.RotationToggle && config.DragMultiplier == 1 && config.GravityToggle && !config.CustomColourToggle) return;

            Rigidbody rb = __instance.GetComponent<Rigidbody>();

            rb.freezeRotation = config.RotationToggle;
            rb.drag = config.DragMultiplier;
            rb.useGravity = config.GravityToggle;

            Renderer renderer = __instance.gameObject.GetComponentInChildren<Renderer>();

            if (renderer && config.CustomColourToggle)
            {
                if (colorType == ColorType.ColorA)
                    ____materialPropertyBlockController.materialPropertyBlock.SetColor(____colorID, config.LeftColour);
                else if (colorType == ColorType.ColorB)
                    ____materialPropertyBlockController.materialPropertyBlock.SetColor(____colorID, config.RightColour);

                ____materialPropertyBlockController.ApplyChanges();
            }
        }
    }
}