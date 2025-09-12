using HarmonyLib;
using UnityEngine;
using System.Reflection;
using DebrisTweaks;


[HarmonyPatch(typeof(NoteDebrisSimplePhysics), "Awake")]
internal static class NoteDebrisSimplePhysics_Awake_Patch
{
    [HarmonyPrefix]
    private static bool Prefix(NoteDebrisSimplePhysics __instance)
    {
        if (!(Config.Instance?.fixDebris ?? false) || !(Config.Instance?.ModToggle ?? false))
            return true;

        __instance.GetType().GetField("_transform", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(__instance, __instance.transform);
        __instance.GetType().GetField("_gravity", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(__instance, Physics.gravity);

        return false; // Skip original
    }
}

[HarmonyPatch(typeof(NoteDebrisSimplePhysics), "LateUpdate")]
internal static class NoteDebrisSimplePhysics_LateUpdate_Patch
{
    [HarmonyPrefix]
    private static bool Prefix(NoteDebrisSimplePhysics __instance)
    {
        if (!(Config.Instance?.fixDebris ?? false) || !(Config.Instance?.ModToggle ?? false))
            return true;

        var firstUpdate = (bool)__instance.GetType().GetField("_firstUpdate", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance);
        if (firstUpdate)
        {
            __instance.GetType().GetField("_firstUpdate", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, false);
            return false;
        }

        float dt = Time.deltaTime;
        var tr = (Transform)__instance.GetType().GetField("_transform", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance) ?? __instance.transform;
        Vector3 linearVelocity = (Vector3)__instance.GetType().GetField("_currentLinearVelocity", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance);
        Vector3 angularVelocityDeg = (Vector3)__instance.GetType().GetField("_currentAngularVelocityDegrees", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance);
        Vector3 gravity = (Vector3)__instance.GetType().GetField("_gravity", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance);

        tr.position += linearVelocity * dt;
        tr.rotation *= Quaternion.Euler(angularVelocityDeg * dt);

        __instance.GetType().GetField("_currentLinearVelocity", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, linearVelocity + gravity * dt);

        return false;
    }
}

[HarmonyPatch(typeof(NoteDebrisSimplePhysics), "Init")]
internal static class NoteDebrisSimplePhysics_Init_Patch
{
    [HarmonyPrefix]
    private static bool Prefix(NoteDebrisSimplePhysics __instance, Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if (!(Config.Instance?.fixDebris ?? false) || !(Config.Instance?.ModToggle ?? false))
            return true;

        __instance.GetType().GetField("_transform", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, __instance.transform);
        __instance.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, __instance.transform.position);
        __instance.GetType().GetField("_rotation", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, __instance.transform.rotation);

        __instance.GetType().GetField("_currentLinearVelocity", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, linearVelocity);
        __instance.GetType().GetField("_currentAngularVelocityDegrees", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, angularVelocity * Mathf.Rad2Deg);

        __instance.GetType().GetField("_firstUpdate", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, true);

        return false;
    }
}


[HarmonyPatch(typeof(NoteDebrisRigidbodyPhysics), "Awake")]
internal static class NoteDebrisRigidbodyPhysics_Awake_Patch
{
    [HarmonyPrefix]
    private static bool Prefix(NoteDebrisRigidbodyPhysics __instance)
    {
        if (!(Config.Instance?.fixDebris ?? false) || !(Config.Instance?.ModToggle ?? false))
            return true;

        var simple = __instance.GetType().GetField("_simplePhysics", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance) as Behaviour;
        if (simple != null) simple.enabled = false;

        return false;
    }
}

[HarmonyPatch(typeof(NoteDebrisRigidbodyPhysics), "FixedUpdate")]
internal static class NoteDebrisRigidbodyPhysics_FixedUpdate_Patch
{
    [HarmonyPrefix]
    private static bool Prefix(NoteDebrisRigidbodyPhysics __instance)
    {
        if (!(Config.Instance?.fixDebris ?? false) || !(Config.Instance?.ModToggle ?? false))
            return true;

        var firstUpdate = (bool)__instance.GetType().GetField("_firstUpdate", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance);
        if (firstUpdate)
        {
            __instance.GetType().GetField("_firstUpdate", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, false);
            return false;
        }

        var simple = __instance.GetType().GetField("_simplePhysics", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance) as Behaviour;
        if (simple != null) simple.enabled = false;

        ((Behaviour)__instance).enabled = false;

        var rb = __instance.GetType().GetField("_rigidbody", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance) as Rigidbody;
        if (rb != null) rb.interpolation = RigidbodyInterpolation.Interpolate;

        return false;
    }
}

[HarmonyPatch(typeof(NoteDebrisRigidbodyPhysics), "Init")]
internal static class NoteDebrisRigidbodyPhysics_Init_Patch
{
    [HarmonyPrefix]
    private static bool Prefix(NoteDebrisRigidbodyPhysics __instance, Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if (!(Config.Instance?.fixDebris ?? false) || !(Config.Instance?.ModToggle ?? false))
            return true;

        __instance.GetType().GetField("_firstUpdate", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(__instance, true);

        var tr = __instance.transform;
        var rb = __instance.GetType().GetField("_rigidbody", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance) as Rigidbody;
        if (rb != null)
        {
            rb.position = tr.position;
            rb.rotation = tr.rotation;
            rb.velocity = linearVelocity;
            rb.angularVelocity = angularVelocity;
        }

        var simpleObj = __instance.GetType().GetField("_simplePhysics", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance);
        if (simpleObj != null)
        {
            var initMethod = simpleObj.GetType().GetMethod("Init", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (initMethod != null)
                initMethod.Invoke(simpleObj, new object[] { linearVelocity, angularVelocity });

            if (simpleObj is Behaviour b) b.enabled = true;
        }

        ((Behaviour)__instance).enabled = true;

        return false;
    }
}