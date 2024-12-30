using System.Runtime.CompilerServices;
using IPA.Config.Stores;
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



    }
}
