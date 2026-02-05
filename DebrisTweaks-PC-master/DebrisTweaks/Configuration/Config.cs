using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;
using System.Reflection;
using System.Linq;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace DebrisTweaks
{
    internal class Config
    {
        public static Config Instance { get; set; }

        // Physics VC
        public bool ModToggle { get; set; } = true;
        public bool fixDebris { get; set; } = false;

        public float minLifetime { get; set; } = 0.2f;
        public float maxLifetime { get; set; } = 2f;
        public float lifeTimeOffset { get; set; } = 0.05f;
        public float rotation { get; set; } = 4f;
        public float cutDirMultiplier { get; set; } = 1.2f;
        public float fromCenterSpeed { get; set; } = 4f;
        public float randomCutFromCenter { get; set; } = 0.1f;

        public float moveSpeedMultiplier { get; set; } = 0.5f;
        public float forceMultiplier { get; set; } = 1f;

        public bool adjustVerticalForceToggle { get; set; } = true;

        public bool randomRotationToggle { get; set; } = false;

        public float randomRotation { get; set; } = 1f;
        public float DebrisScale { get; set; } = 1f;
        public bool dynamicDebrisToggle { get; set; } = false;
        public float saberSens { get; set; } = 0.4f;

        public float Drag { get; set; } = 0f;
        public bool RandomDrag { get; set; } = false;
        public float DragMin { get; set; } = 1f;
        public float DragMax { get; set; } = 1f;

        public bool GravityToggle { get; set; } = true;
        public bool RotationToggle { get; set; } = false;

        // Debris Position VC
        public float debrisOffsetX { get; set; } = 0f;
        public float debrisOffsetY { get; set; } = 0f;
        public float debrisOffsetZ { get; set; } = 0f;

        // Cosmetics VC
        public Color LeftColour { get; set; } = Color.red;
        public Color RightColour { get; set; } = Color.blue;
        public bool CustomColourToggle { get; set; } = false;

        public bool disableDissolveAnim { get; set; } = false;
        public bool downscaleDespawnAnim { get; set; } = false;
        public bool overrideDissolveNoiseScale { get; set; } = false;
        public float dissolveNoise { get; set; } = 0.3f;


        //profile garbage
        public float profile_value { get; set; } = 1f;
        public bool ModToggle1 { get; set; } = true; public float debrisOffsetX1 { get; set; } = 0f; public float debrisOffsetY1 { get; set; } = 0f; public float debrisOffsetZ1 { get; set; } = 0f; public bool fixDebris1 { get; set; } = false; public bool disableDissolveAnim1 { get; set; } = false; public bool downscaleDespawnAnim1 { get; set; } = false; public bool overrideDissolveNoiseScale1 { get; set; } = false; public float dissolveNoise1 { get; set; } = 0.3f; public float minLifetime1 { get; set; } = 0.2f; public float maxLifetime1 { get; set; } = 2f; public float lifeTimeOffset1 { get; set; } = 0.05f; public float rotation1 { get; set; } = 4f; public float cutDirMultiplier1 { get; set; } = 1.2f; public float fromCenterSpeed1 { get; set; } = 4f; public float randomCutFromCenter1 { get; set; } = 0.1f; public float moveSpeedMultiplier1 { get; set; } = 0.5f; public float forceMultiplier1 { get; set; } = 1f; public bool adjustVerticalForceToggle1 { get; set; } = true; public bool randomRotationToggle1 { get; set; } = false; public float randomRotation1 { get; set; } = 1f; public float DebrisScale1 { get; set; } = 1f; public bool dynamicDebrisToggle1 { get; set; } = false; public float saberSens1 { get; set; } = 0.4f; public float Drag1 { get; set; } = 1f; public bool RandomDrag1 { get; set; } = false; public float DragMin1 { get; set; } = 1f; public float DragMax1 { get; set; } = 1f; public bool GravityToggle1 { get; set; } = true; public bool RotationToggle1 { get; set; } = false; public Color LeftColour1 { get; set; } = Color.red; public Color RightColour1 { get; set; } = Color.blue; public bool CustomColourToggle1 { get; set; } = false;
        public bool ModToggle2 { get; set; } = true; public float debrisOffsetX2 { get; set; } = 0f; public float debrisOffsetY2 { get; set; } = 0f; public float debrisOffsetZ2 { get; set; } = 0f; public bool fixDebris2 { get; set; } = false; public bool disableDissolveAnim2 { get; set; } = false; public bool downscaleDespawnAnim2 { get; set; } = false; public bool overrideDissolveNoiseScale2 { get; set; } = false; public float dissolveNoise2 { get; set; } = 0.3f; public float minLifetime2 { get; set; } = 0.2f; public float maxLifetime2 { get; set; } = 2f; public float lifeTimeOffset2 { get; set; } = 0.05f; public float rotation2 { get; set; } = 4f; public float cutDirMultiplier2 { get; set; } = 1.2f; public float fromCenterSpeed2 { get; set; } = 4f; public float randomCutFromCenter2 { get; set; } = 0.1f; public float moveSpeedMultiplier2 { get; set; } = 0.5f; public float forceMultiplier2 { get; set; } = 1f; public bool adjustVerticalForceToggle2 { get; set; } = true; public bool randomRotationToggle2 { get; set; } = false; public float randomRotation2 { get; set; } = 1f; public float DebrisScale2 { get; set; } = 1f; public bool dynamicDebrisToggle2 { get; set; } = false; public float saberSens2 { get; set; } = 0.4f; public float Drag2 { get; set; } = 1f; public bool RandomDrag2 { get; set; } = false; public float DragMin2 { get; set; } = 1f; public float DragMax2 { get; set; } = 1f; public bool GravityToggle2 { get; set; } = true; public bool RotationToggle2 { get; set; } = false; public Color LeftColour2 { get; set; } = Color.red; public Color RightColour2 { get; set; } = Color.blue; public bool CustomColourToggle2 { get; set; } = false;
        public bool ModToggle3 { get; set; } = true; public float debrisOffsetX3 { get; set; } = 0f; public float debrisOffsetY3 { get; set; } = 0f; public float debrisOffsetZ3 { get; set; } = 0f; public bool fixDebris3 { get; set; } = false; public bool disableDissolveAnim3 { get; set; } = false; public bool downscaleDespawnAnim3 { get; set; } = false; public bool overrideDissolveNoiseScale3 { get; set; } = false; public float dissolveNoise3 { get; set; } = 0.3f; public float minLifetime3 { get; set; } = 0.2f; public float maxLifetime3 { get; set; } = 2f; public float lifeTimeOffset3 { get; set; } = 0.05f; public float rotation3 { get; set; } = 4f; public float cutDirMultiplier3 { get; set; } = 1.2f; public float fromCenterSpeed3 { get; set; } = 4f; public float randomCutFromCenter3 { get; set; } = 0.1f; public float moveSpeedMultiplier3 { get; set; } = 0.5f; public float forceMultiplier3 { get; set; } = 1f; public bool adjustVerticalForceToggle3 { get; set; } = true; public bool randomRotationToggle3 { get; set; } = false; public float randomRotation3 { get; set; } = 1f; public float DebrisScale3 { get; set; } = 1f; public bool dynamicDebrisToggle3 { get; set; } = false; public float saberSens3 { get; set; } = 0.4f; public float Drag3 { get; set; } = 1f; public bool RandomDrag3 { get; set; } = false; public float DragMin3 { get; set; } = 1f; public float DragMax3 { get; set; } = 1f; public bool GravityToggle3 { get; set; } = true; public bool RotationToggle3 { get; set; } = false; public Color LeftColour3 { get; set; } = Color.red; public Color RightColour3 { get; set; } = Color.blue; public bool CustomColourToggle3 { get; set; } = false;
        public bool ModToggle4 { get; set; } = true; public float debrisOffsetX4 { get; set; } = 0f; public float debrisOffsetY4 { get; set; } = 0f; public float debrisOffsetZ4 { get; set; } = 0f; public bool fixDebris4 { get; set; } = false; public bool disableDissolveAnim4 { get; set; } = false; public bool downscaleDespawnAnim4 { get; set; } = false; public bool overrideDissolveNoiseScale4 { get; set; } = false; public float dissolveNoise4 { get; set; } = 0.3f; public float minLifetime4 { get; set; } = 0.2f; public float maxLifetime4 { get; set; } = 2f; public float lifeTimeOffset4 { get; set; } = 0.05f; public float rotation4 { get; set; } = 4f; public float cutDirMultiplier4 { get; set; } = 1.2f; public float fromCenterSpeed4 { get; set; } = 4f; public float randomCutFromCenter4 { get; set; } = 0.1f; public float moveSpeedMultiplier4 { get; set; } = 0.5f; public float forceMultiplier4 { get; set; } = 1f; public bool adjustVerticalForceToggle4 { get; set; } = true; public bool randomRotationToggle4 { get; set; } = false; public float randomRotation4 { get; set; } = 1f; public float DebrisScale4 { get; set; } = 1f; public bool dynamicDebrisToggle4 { get; set; } = false; public float saberSens4 { get; set; } = 0.4f; public float Drag4 { get; set; } = 1f; public bool RandomDrag4 { get; set; } = false; public float DragMin4 { get; set; } = 1f; public float DragMax4 { get; set; } = 1f; public bool GravityToggle4 { get; set; } = true; public bool RotationToggle4 { get; set; } = false; public Color LeftColour4 { get; set; } = Color.red; public Color RightColour4 { get; set; } = Color.blue; public bool CustomColourToggle4 { get; set; } = false;
        public bool ModToggle5 { get; set; } = true; public float debrisOffsetX5 { get; set; } = 0f; public float debrisOffsetY5 { get; set; } = 0f; public float debrisOffsetZ5 { get; set; } = 0f; public bool fixDebris5 { get; set; } = false; public bool disableDissolveAnim5 { get; set; } = false; public bool downscaleDespawnAnim5 { get; set; } = false; public bool overrideDissolveNoiseScale5 { get; set; } = false; public float dissolveNoise5 { get; set; } = 0.3f; public float minLifetime5 { get; set; } = 0.2f; public float maxLifetime5 { get; set; } = 2f; public float lifeTimeOffset5 { get; set; } = 0.05f; public float rotation5 { get; set; } = 4f; public float cutDirMultiplier5 { get; set; } = 1.2f; public float fromCenterSpeed5 { get; set; } = 4f; public float randomCutFromCenter5 { get; set; } = 0.1f; public float moveSpeedMultiplier5 { get; set; } = 0.5f; public float forceMultiplier5 { get; set; } = 1f; public bool adjustVerticalForceToggle5 { get; set; } = true; public bool randomRotationToggle5 { get; set; } = false; public float randomRotation5 { get; set; } = 1f; public float DebrisScale5 { get; set; } = 1f; public bool dynamicDebrisToggle5 { get; set; } = false; public float saberSens5 { get; set; } = 0.4f; public float Drag5 { get; set; } = 1f; public bool RandomDrag5 { get; set; } = false; public float DragMin5 { get; set; } = 1f; public float DragMax5 { get; set; } = 1f; public bool GravityToggle5 { get; set; } = true; public bool RotationToggle5 { get; set; } = false; public Color LeftColour5 { get; set; } = Color.red; public Color RightColour5 { get; set; } = Color.blue; public bool CustomColourToggle5 { get; set; } = false;

        // Quick helpers to copy values to/from the numbered profile fields.
        // These use reflection so adding new base properties doesn't require editing these methods.
        public void SaveProfile(int slot)
        {
            if (slot < 1) slot = 1;
            var suffix = slot.ToString();
            var allProps = typeof(Config).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var baseProp in allProps)
            {
                // skip base properties that are already the numbered ones, and profile_value
                if (baseProp.Name == nameof(profile_value)) continue;
                if (baseProp.Name.EndsWith(suffix)) continue;

                var targetProp = typeof(Config).GetProperty(baseProp.Name + suffix, BindingFlags.Public | BindingFlags.Instance);
                if (targetProp == null) continue;
                if (!targetProp.CanWrite) continue;
                if (targetProp.PropertyType != baseProp.PropertyType) continue;

                var val = baseProp.GetValue(this);
                targetProp.SetValue(this, val);
            }
        }

        public void LoadAllProfile(int slot)
        {
            if (slot < 1) slot = 1;
            var suffix = slot.ToString();
            var allProps = typeof(Config).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var baseProp in allProps)
            {
                // skip base properties that are already the numbered ones, and profile_value
                if (baseProp.Name == nameof(profile_value)) continue;
                if (baseProp.Name.EndsWith(suffix)) continue;

                var sourceProp = typeof(Config).GetProperty(baseProp.Name + suffix, BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp == null) continue;
                if (!sourceProp.CanRead) continue;
                if (sourceProp.PropertyType != baseProp.PropertyType) continue;

                var val = sourceProp.GetValue(this);
                baseProp.SetValue(this, val);
            }
        }

        // --- New: view-scoped load helpers (only load specific screen fields) --

        // Copies from numbered slot into active config but only for Main view properties.
        public void LoadProfileMain(int slot)
        {
            var keys = new[]
            {
                "ModToggle",
                "fixDebris",
                "forceMultiplier",
                "Drag",
                "RandomDrag",
                "DragMin",
                "DragMax",
                "GravityToggle",
                "RotationToggle",
                "debrisOffsetX",
                "debrisOffsetZ",
                "debrisOffsetY",
            };
            LoadSelectedFromSlot(slot, keys);
        }

        // Side view (colors, lifetime, debris scale)
        public void LoadProfileSide(int slot)
        {
            var keys = new[]
            {
                "CustomColourToggle",
                "LeftColour",
                "RightColour",
                "minLifetime",
                "maxLifetime",
                "lifeTimeOffset",
                "DebrisScale",
                "disableDissolveAnim",
                "downscaleDespawnAnim",
                "overrideDissolveNoiseScale",
                "dissolveNoise"
            };
            LoadSelectedFromSlot(slot, keys);
        }

        // Left side view (movement / rotation)
        public void LoadProfileLeft(int slot)
        {
            var keys = new[]
            {
                "moveSpeedMultiplier",
                "adjustVerticalForceToggle",
                "cutDirMultiplier",
                "fromCenterSpeed",
                "rotation",
                "randomRotationToggle",
                "randomRotation",
                "randomCutFromCenter",
                "dynamicDebrisToggle",
                "saberSens"
            };
            LoadSelectedFromSlot(slot, keys);
        }

        private void LoadSelectedFromSlot(int slot, IEnumerable<string> propertyNames)
        {
            if (slot < 1) slot = 1;
            var suffix = slot.ToString();
            foreach (var name in propertyNames)
            {
                var sourceProp = typeof(Config).GetProperty(name + suffix, BindingFlags.Public | BindingFlags.Instance);
                var baseProp = typeof(Config).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp == null || baseProp == null) continue;
                if (!sourceProp.CanRead || !baseProp.CanWrite) continue;
                if (sourceProp.PropertyType != baseProp.PropertyType) continue;
                var val = sourceProp.GetValue(this);
                baseProp.SetValue(this, val);
            }
        }
    }
}
