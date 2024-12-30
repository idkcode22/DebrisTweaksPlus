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
        [UIValue("moveSpeedMultiplier")]
        private float moveSpeedMultiplier
        {
            get => config.moveSpeedMultiplier;
            set => config.moveSpeedMultiplier = value;
        }

        [UIValue("cutDirMultiplier")]
        private float cutDirMultiplier
        {
            get => config.cutDirMultiplier;
            set => config.cutDirMultiplier = value;
        }

        [UIValue("saberSens")]
        private float saberSens
        {
            get => config.saberSens;
            set => config.saberSens = value;
        }


        [UIValue("GravityToggle")]
        private bool GravityToggle
        {
            get => config.GravityToggle;
            set => config.GravityToggle = value;
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
        [UIAction("RefreshMain")]
        private void refreshMainMenu()
        {
            refreshMain = true;
        }
        [UIAction("RefreshOnline")]
        private void refresh()
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