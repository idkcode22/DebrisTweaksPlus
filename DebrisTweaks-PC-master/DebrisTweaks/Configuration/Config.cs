using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace DebrisTweaks
{
    internal class Config
    {
        public static Config Instance { get; set; }

        // Physics VC
        public bool ModToggle { get; set; } = true;
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
        public float saberSens { get; set; } = 40f;

        public float DragMultiplier { get; set; } = 1f;
        public bool GravityToggle { get; set; } = true;
        public bool RotationToggle { get; set; } = false;


        // Cosmetics VC
        public Color LeftColour { get; set; } = Color.red;
        public Color RightColour { get; set; } = Color.blue;
        public bool CustomColourToggle { get; set; } = false;
        //profile garbage
        public float profile_value { get; set; } = 1f;

        public bool ModToggle1 { get; set; } = true; public float minLifetime1 { get; set; } = 0.2f; public float maxLifetime1 { get; set; } = 2f; public float lifeTimeOffset1 { get; set; } = 0.05f; public float rotation1 { get; set; } = 4f; public float cutDirMultiplier1 { get; set; } = 1.2f; public float fromCenterSpeed1 { get; set; } = 4f; public float randomCutFromCenter1 { get; set; } = 0.1f; public float moveSpeedMultiplier1 { get; set; } = 0.5f; public float forceMultiplier1 { get; set; } = 1f; public bool adjustVerticalForceToggle1 { get; set; } = true; public bool randomRotationToggle1 { get; set; } = false; public float randomRotation1 { get; set; } = 1f; public float DebrisScale1 { get; set; } = 1f; public bool dynamicDebrisToggle1 { get; set; } = false; public float saberSens1 { get; set; } = 40f; public float DragMultiplier1 { get; set; } = 1f; public bool GravityToggle1 { get; set; } = true; public bool RotationToggle1 { get; set; } = false; public Color LeftColour1 { get; set; } = Color.red; public Color RightColour1 { get; set; } = Color.blue; public bool CustomColourToggle1 { get; set; } = false;

        public bool ModToggle2 { get; set; } = true; public float minLifetime2 { get; set; } = 0.2f; public float maxLifetime2 { get; set; } = 2f; public float lifeTimeOffset2 { get; set; } = 0.05f; public float rotation2 { get; set; } = 4f; public float cutDirMultiplier2 { get; set; } = 1.2f; public float fromCenterSpeed2 { get; set; } = 4f; public float randomCutFromCenter2 { get; set; } = 0.1f; public float moveSpeedMultiplier2 { get; set; } = 0.5f; public float forceMultiplier2 { get; set; } = 1f; public bool adjustVerticalForceToggle2 { get; set; } = true; public bool randomRotationToggle2 { get; set; } = false; public float randomRotation2 { get; set; } = 1f; public float DebrisScale2 { get; set; } = 1f; public bool dynamicDebrisToggle2 { get; set; } = false; public float saberSens2 { get; set; } = 40f; public float DragMultiplier2 { get; set; } = 1f; public bool GravityToggle2 { get; set; } = true; public bool RotationToggle2 { get; set; } = false; public Color LeftColour2 { get; set; } = Color.red; public Color RightColour2 { get; set; } = Color.blue; public bool CustomColourToggle2 { get; set; } = false;

        public bool ModToggle3 { get; set; } = true; public float minLifetime3 { get; set; } = 0.2f; public float maxLifetime3 { get; set; } = 2f; public float lifeTimeOffset3 { get; set; } = 0.05f; public float rotation3 { get; set; } = 4f; public float cutDirMultiplier3 { get; set; } = 1.2f; public float fromCenterSpeed3 { get; set; } = 4f; public float randomCutFromCenter3 { get; set; } = 0.1f; public float moveSpeedMultiplier3 { get; set; } = 0.5f; public float forceMultiplier3 { get; set; } = 1f; public bool adjustVerticalForceToggle3 { get; set; } = true; public bool randomRotationToggle3 { get; set; } = false; public float randomRotation3 { get; set; } = 1f; public float DebrisScale3 { get; set; } = 1f; public bool dynamicDebrisToggle3 { get; set; } = false; public float saberSens3 { get; set; } = 40f; public float DragMultiplier3 { get; set; } = 1f; public bool GravityToggle3 { get; set; } = true; public bool RotationToggle3 { get; set; } = false; public Color LeftColour3 { get; set; } = Color.red; public Color RightColour3 { get; set; } = Color.blue; public bool CustomColourToggle3 { get; set; } = false;

        public bool ModToggle4 { get; set; } = true; public float minLifetime4 { get; set; } = 0.2f; public float maxLifetime4 { get; set; } = 2f; public float lifeTimeOffset4 { get; set; } = 0.05f; public float rotation4 { get; set; } = 4f; public float cutDirMultiplier4 { get; set; } = 1.2f; public float fromCenterSpeed4 { get; set; } = 4f; public float randomCutFromCenter4 { get; set; } = 0.1f; public float moveSpeedMultiplier4 { get; set; } = 0.5f; public float forceMultiplier4 { get; set; } = 1f; public bool adjustVerticalForceToggle4 { get; set; } = true; public bool randomRotationToggle4 { get; set; } = false; public float randomRotation4 { get; set; } = 1f; public float DebrisScale4 { get; set; } = 1f; public bool dynamicDebrisToggle4 { get; set; } = false; public float saberSens4 { get; set; } = 40f; public float DragMultiplier4 { get; set; } = 1f; public bool GravityToggle4 { get; set; } = true; public bool RotationToggle4 { get; set; } = false; public Color LeftColour4 { get; set; } = Color.red; public Color RightColour4 { get; set; } = Color.blue; public bool CustomColourToggle4 { get; set; } = false;

        public bool ModToggle5 { get; set; } = true; public float minLifetime5 { get; set; } = 0.2f; public float maxLifetime5 { get; set; } = 2f; public float lifeTimeOffset5 { get; set; } = 0.05f; public float rotation5 { get; set; } = 4f; public float cutDirMultiplier5 { get; set; } = 1.2f; public float fromCenterSpeed5 { get; set; } = 4f; public float randomCutFromCenter5 { get; set; } = 0.1f; public float moveSpeedMultiplier5 { get; set; } = 0.5f; public float forceMultiplier5 { get; set; } = 1f; public bool adjustVerticalForceToggle5 { get; set; } = true; public bool randomRotationToggle5 { get; set; } = false; public float randomRotation5 { get; set; } = 1f; public float DebrisScale5 { get; set; } = 1f; public bool dynamicDebrisToggle5 { get; set; } = false; public float saberSens5 { get; set; } = 40f; public float DragMultiplier5 { get; set; } = 1f; public bool GravityToggle5 { get; set; } = true; public bool RotationToggle5 { get; set; } = false; public Color LeftColour5 { get; set; } = Color.red; public Color RightColour5 { get; set; } = Color.blue; public bool CustomColourToggle5 { get; set; } = false;


    }
}
