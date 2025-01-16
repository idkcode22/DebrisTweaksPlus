using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using Zenject;
using System;
using System.ComponentModel;
using HMUI;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Parser;
using UnityEngine.UIElements;
using System.Reflection;
using UnityEngine;
using System.Linq;
using BeatSaberMarkupLanguage.Components;
using JetBrains.Annotations;
using IPA.Config;
using DebrisTweaks.UI;
using System.IO.Enumeration;
namespace DebrisTweaks.OnlineUI
{
    [UsedImplicitly]
    public class GameplaySetupPanel : NotifiableBase, IInitializable, IDisposable
    {
        public static bool refreshMain = false;
        private const string TabName = "Debris Tweaks+";
        private const string BsmlResource = "DebrisTweaks.UI.OnlineUI.bsml";

        [Inject] public readonly GameplaySetup _gameplaySetup = null!;

        public void Initialize()
        {
                _gameplaySetup.AddTab(
                name: TabName,
                resource: BsmlResource,
                host: this,
                menuType: MenuType.Online
            );
        }

        public void Dispose()
        {
            _gameplaySetup.RemoveTab(TabName);
        }

        Config config = Config.Instance;
        [UIValue("ModToggle")]
        private bool ModToggle
        {
            get => config.ModToggle;
            set => config.ModToggle = value;
        }



        [UIValue("minLifetime")]
        private float minLifetime
        {
            get => config.minLifetime;
            set => config.minLifetime = value;
        }


        [UIValue("maxLifetime")]
        private float maxLifetime
        {
            get => config.maxLifetime;
            set => config.maxLifetime = value;
        }

        [UIValue("lifeTimeOffset")]
        private float lifeTimeOffset
        {
            get => config.lifeTimeOffset;
            set => config.lifeTimeOffset = value;
        }

        [UIValue("profile_value")]
        public float profile_value
        {
            get => config.profile_value;
            set => config.profile_value = value;
        }
        [UIAction("loadclicked")]
        private void loadProfile()
        {
            refreshMain = true;
            if (profile_value == 1f)
            {

                config.ModToggle = config.ModToggle1;
                config.forceMultiplier = config.forceMultiplier1;
                config.DragMultiplier = config.DragMultiplier1;
                config.GravityToggle = config.GravityToggle1;
                config.RotationToggle = config.RotationToggle1;
                config.CustomColourToggle = config.CustomColourToggle1;
                config.LeftColour = config.LeftColour1;
                config.RightColour = config.RightColour1;
                config.minLifetime = config.minLifetime1;
                config.maxLifetime = config.maxLifetime1;
                config.lifeTimeOffset = config.lifeTimeOffset1;
                config.DebrisScale = config.DebrisScale1;
                config.moveSpeedMultiplier = config.moveSpeedMultiplier1;
                config.adjustVerticalForceToggle = config.adjustVerticalForceToggle1;
                config.cutDirMultiplier = config.cutDirMultiplier1;
                config.fromCenterSpeed = config.fromCenterSpeed1;
                config.rotation = config.rotation1;
                config.randomRotationToggle = config.randomRotationToggle1;
                config.randomRotation = config.randomRotation1;
                config.randomCutFromCenter = config.randomCutFromCenter1;
                config.dynamicDebrisToggle = config.dynamicDebrisToggle1;
                config.saberSens = config.saberSens1;
            }

            if (profile_value == 2f)
            {
                config.ModToggle = config.ModToggle2;
                config.forceMultiplier = config.forceMultiplier2;
                config.DragMultiplier = config.DragMultiplier2;
                config.GravityToggle = config.GravityToggle2;
                config.RotationToggle = config.RotationToggle2;
                config.CustomColourToggle = config.CustomColourToggle2;
                config.LeftColour = config.LeftColour2;
                config.RightColour = config.RightColour2;
                config.minLifetime = config.minLifetime2;
                config.maxLifetime = config.maxLifetime2;
                config.lifeTimeOffset = config.lifeTimeOffset2;
                config.DebrisScale = config.DebrisScale2;
                config.moveSpeedMultiplier = config.moveSpeedMultiplier2;
                config.adjustVerticalForceToggle = config.adjustVerticalForceToggle2;
                config.cutDirMultiplier = config.cutDirMultiplier2;
                config.fromCenterSpeed = config.fromCenterSpeed2;
                config.rotation = config.rotation2;
                config.randomRotationToggle = config.randomRotationToggle2;
                config.randomRotation = config.randomRotation2;
                config.randomCutFromCenter = config.randomCutFromCenter2;
                config.dynamicDebrisToggle = config.dynamicDebrisToggle2;
                config.saberSens = config.saberSens2;
            }

            if (profile_value == 3f)
            {
                config.ModToggle = config.ModToggle3;
                config.forceMultiplier = config.forceMultiplier3;
                config.DragMultiplier = config.DragMultiplier3;
                config.GravityToggle = config.GravityToggle3;
                config.RotationToggle = config.RotationToggle3;
                config.CustomColourToggle = config.CustomColourToggle3;
                config.LeftColour = config.LeftColour3;
                config.RightColour = config.RightColour3;
                config.minLifetime = config.minLifetime3;
                config.maxLifetime = config.maxLifetime3;
                config.lifeTimeOffset = config.lifeTimeOffset3;
                config.DebrisScale = config.DebrisScale3;
                config.moveSpeedMultiplier = config.moveSpeedMultiplier3;
                config.adjustVerticalForceToggle = config.adjustVerticalForceToggle3;
                config.cutDirMultiplier = config.cutDirMultiplier3;
                config.fromCenterSpeed = config.fromCenterSpeed3;
                config.rotation = config.rotation3;
                config.randomRotationToggle = config.randomRotationToggle3;
                config.randomRotation = config.randomRotation3;
                config.randomCutFromCenter = config.randomCutFromCenter3;
                config.dynamicDebrisToggle = config.dynamicDebrisToggle3;
                config.saberSens = config.saberSens3;
            }

            if (profile_value == 4f)
            {
                config.ModToggle = config.ModToggle4;
                config.forceMultiplier = config.forceMultiplier4;
                config.DragMultiplier = config.DragMultiplier4;
                config.GravityToggle = config.GravityToggle4;
                config.RotationToggle = config.RotationToggle4;
                config.CustomColourToggle = config.CustomColourToggle4;
                config.LeftColour = config.LeftColour4;
                config.RightColour = config.RightColour4;
                config.minLifetime = config.minLifetime4;
                config.maxLifetime = config.maxLifetime4;
                config.lifeTimeOffset = config.lifeTimeOffset4;
                config.DebrisScale = config.DebrisScale4;
                config.moveSpeedMultiplier = config.moveSpeedMultiplier4;
                config.adjustVerticalForceToggle = config.adjustVerticalForceToggle4;
                config.cutDirMultiplier = config.cutDirMultiplier4;
                config.fromCenterSpeed = config.fromCenterSpeed4;
                config.rotation = config.rotation4;
                config.randomRotationToggle = config.randomRotationToggle4;
                config.randomRotation = config.randomRotation4;
                config.randomCutFromCenter = config.randomCutFromCenter4;
                config.dynamicDebrisToggle = config.dynamicDebrisToggle4;
                config.saberSens = config.saberSens4;
            }

            if (profile_value == 5f)
            {
                config.ModToggle = config.ModToggle5;
                config.forceMultiplier = config.forceMultiplier5;
                config.DragMultiplier = config.DragMultiplier5;
                config.GravityToggle = config.GravityToggle5;
                config.RotationToggle = config.RotationToggle5;
                config.CustomColourToggle = config.CustomColourToggle5;
                config.LeftColour = config.LeftColour5;
                config.RightColour = config.RightColour5;
                config.minLifetime = config.minLifetime5;
                config.maxLifetime = config.maxLifetime5;
                config.lifeTimeOffset = config.lifeTimeOffset5;
                config.DebrisScale = config.DebrisScale5;
                config.moveSpeedMultiplier = config.moveSpeedMultiplier5;
                config.adjustVerticalForceToggle = config.adjustVerticalForceToggle5;
                config.cutDirMultiplier = config.cutDirMultiplier5;
                config.fromCenterSpeed = config.fromCenterSpeed5;
                config.rotation = config.rotation5;
                config.randomRotationToggle = config.randomRotationToggle5;
                config.randomRotation = config.randomRotation5;
                config.randomCutFromCenter = config.randomCutFromCenter5;
                config.dynamicDebrisToggle = config.dynamicDebrisToggle5;
                config.saberSens = config.saberSens5;
            }
        }

        [UIAction("RefreshMain")]
        private void refreshMainMenu()
        {
            refreshMain = true;
        }
        [UIAction("RefreshOnline")]
        public void refresh()
        {

             _gameplaySetup.RemoveTab(TabName);
             _gameplaySetup.AddTab(
             name: TabName,
             resource: BsmlResource,
             host: this,
             menuType: MenuType.Online);
             refreshMain = true;
        }

    }
}
