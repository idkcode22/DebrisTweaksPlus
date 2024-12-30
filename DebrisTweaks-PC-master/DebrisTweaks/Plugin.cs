using IPA;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using System.Reflection;
using DebrisTweaks.UI;
using IPA.Config.Stores;
using BeatSaberMarkupLanguage.MenuButtons;
using SiraUtil.Zenject;
using DebrisTweaks.OnlineUI;
using DebrisTweaksMenuInstall;

namespace DebrisTweaks
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony harmony;

        [Init]
        public Plugin(IPA.Config.Config conf, IPALogger logger, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            harmony = new Harmony("IDK.BeatSaber.DebrisTweaks+");
            Config.Instance = conf.Generated<Config>();
            zenjector.Install<DebrisTweaksMenuInstaller>(Location.Menu);
        }

        [OnStart]
        public void OnApplicationStart()
        {
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            // Wait for MenuButtons to be ready before enabling the UI
            BeatSaberMarkupLanguage.Util.MainMenuAwaiter.MainMenuInitializing += OnMenuButtonRegistered;
        }

        private void OnMenuButtonRegistered()
        {
            BsmlWrapper.EnableUI();
        }


        [OnDisable]
        public void OnDisable()
        {
            harmony.UnpatchSelf();
            BsmlWrapper.DisableUI();

            // Unsubscribe from the event to avoid issues when reloading
            BeatSaberMarkupLanguage.Util.MainMenuAwaiter.MainMenuInitializing -= OnMenuButtonRegistered;
        }

    }
}
