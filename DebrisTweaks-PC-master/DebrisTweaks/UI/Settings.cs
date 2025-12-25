using System.Linq;
using System.Reflection;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberMarkupLanguage.ViewControllers;
using DebrisTweaks.OnlineUI;
using HMUI;
using JetBrains.Annotations;
using UnityEngine;


namespace DebrisTweaks.UI
{
    internal class DTFlow : FlowCoordinator
    {
        DTMainView mainView = null;
        DTSideView sideView = null;
        DTLeftSideView leftSideView = null;

        public static bool refreshOnline;
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            SetTitle("Debris Tweaks+");
            showBackButton = true;

            if (mainView == null)
                mainView = BeatSaberUI.CreateViewController<DTMainView>();
            if (sideView == null)
                sideView = BeatSaberUI.CreateViewController<DTSideView>();
            if (leftSideView == null)
                leftSideView = BeatSaberUI.CreateViewController<DTLeftSideView>();

            ProvideInitialViewControllers(mainView, leftSideView, sideView);
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal);
        }



        private void ShowFlow()
        {

            var _parentFlow = BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            BeatSaberUI.PresentFlowCoordinator(_parentFlow, this);
            if (GameplaySetupPanel.refreshMain)
            {
                mainView?.RefreshUI("DebrisTweaks.UI.MainView.bsml");
                sideView?.RefreshUI("DebrisTweaks.UI.SideView.bsml");
                leftSideView?.RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");
                GameplaySetupPanel.refreshMain = false;
            }

        }


        static DTFlow flow = null;
        static MenuButton menuButton;

        public static void Initialise()
        {
            MenuButtons.Instance.RegisterButton(menuButton ??= new MenuButton("Debris Tweaks+", "Add more tweaks to the debris!", () =>
            {
                if (flow == null)
                    flow = BeatSaberUI.CreateFlowCoordinator<DTFlow>();
                flow.ShowFlow();
            }, true));
        }

        public static void Deinit()
        {
            if (menuButton != null)
                MenuButtons.Instance.UnregisterButton(menuButton);
        }


    }

    [HotReload(RelativePathToLayout = @"./MainView.bsml")]
    [ViewDefinition("DebrisTweaks.UI.MainView.bsml")]
    internal class DTMainView : BSMLAutomaticViewController
    {
        Config config = Config.Instance;

        [UIValue("ModToggle")]
        private bool ModToggle
        {
            get => config.ModToggle;
            set => config.ModToggle = value;
        }

        [UIValue("FixDebris")]
        private bool FixDebris
        {
            get => config.fixDebris;
            set => config.fixDebris = value;
        }

        [UIValue("VelocityMult")]
        private float forceMultiplier
        {
            get => config.forceMultiplier;
            set => config.forceMultiplier = value;
        }


        [UIValue("Drag")]
        private float Drag
        {
            get => config.Drag;
            set => config.Drag = value;
        }
        [UIValue("RandomDrag")]
        private bool RandomDrag
        {
            get => config.RandomDrag;
            set => config.RandomDrag = value;
        }
        [UIValue("DragMin")]
        private float DragMin
        {
            get => config.DragMin;
            set => config.DragMin = value;
        }
        [UIValue("DragMax")]
        private float DragMax
        {
            get => config.DragMax;
            set => config.DragMax = value;
        }
        [UIValue("GravityToggle")]
        private bool GravityToggle
        {
            get => config.GravityToggle;
            set => config.GravityToggle = value;
        }

        [UIValue("RotationToggle")]
        private bool RotationToggle
        {
            get => config.RotationToggle;
            set => config.RotationToggle = value;
        }

        [UIValue("profile_value")]
        public float profile_value
        {
            get => config.profile_value;
            set => config.profile_value = value;
        }
        //profile stuff
        [UIAction("loadclicked")]
        private void LoadProfile()
        {
            int idx = (int)profile_value;
            config.LoadProfileMain(idx);

            RefreshUI("DebrisTweaks.UI.MainView.bsml");
        }

        [UIAction("saveclicked")]
        private void SaveProfile()
        {
            int idx = (int)profile_value;
            config.SaveProfile(idx);
        }


        public void RefreshUI(string BSMLLocation)
        {
            foreach (var child in gameObject.transform.Cast<Transform>().ToList())
            {
                GameObject.Destroy(child.gameObject);
            }

            BSMLParser.Instance.Parse(Utilities.GetResourceContent(
            Assembly.GetExecutingAssembly(), BSMLLocation), this.gameObject, this);
        }


    }

    [HotReload(RelativePathToLayout = @"./SideView.bsml")]
    [ViewDefinition("DebrisTweaks.UI.SideView.bsml")]
    internal class DTSideView : BSMLAutomaticViewController
    {
        Config config = Config.Instance;

        [UIValue("CustomColourToggle")]
        private bool CustomColourToggle
        {
            get => config.CustomColourToggle;
            set => config.CustomColourToggle = value;
        }

        [UIValue("LeftColour")]
        private Color LeftColour
        {
            get => config.LeftColour;
            set => config.LeftColour = value;
        }

        [UIValue("RightColour")]
        private Color RightColour
        {
            get => config.RightColour;
            set => config.RightColour = value;
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

        [UIValue("DebrisScale")]
        public float DebrisScale
        {
            get => config.DebrisScale;
            set => config.DebrisScale = value;
        }



        [UIAction("resetMinLifeTime")]
        private void resetMinLifeTime()
        {
            minLifetime = 0.2f;
            RefreshUI("DebrisTweaks.UI.SideView.bsml");

        }
        [UIAction("resetMaxLifeTime")]
        private void resetMaxLifeTime()
        {
            maxLifetime = 2f;
            RefreshUI("DebrisTweaks.UI.SideView.bsml");

        }
        [UIAction("resetLifeTimeOffset")]
        private void resetLifeTimeOffset()
        {
            lifeTimeOffset = 0.05f;
            RefreshUI("DebrisTweaks.UI.SideView.bsml");

        }

        // profile stuff
        [UIAction("loadclicked")]
        private void LoadProfile()
        {
            int idx = (int)config.profile_value;
            config.LoadProfileSide(idx);
            RefreshUI("DebrisTweaks.UI.SideView.bsml");
        }

        public void RefreshUI(string BSMLLocation)
        {
            foreach (var child in gameObject.transform.Cast<Transform>().ToList())
            {
                GameObject.Destroy(child.gameObject);
            }

            BSMLParser.Instance.Parse(Utilities.GetResourceContent(
            Assembly.GetExecutingAssembly(), BSMLLocation), this.gameObject, this);
        }
    }


    [HotReload(RelativePathToLayout = @"./LeftSideView.bsml")]
    [ViewDefinition("DebrisTweaks.UI.LeftSideView.bsml")]
    internal class DTLeftSideView : BSMLAutomaticViewController
    {
        Config config = Config.Instance;

        [UIValue("moveSpeedMultiplier")]
        private float moveSpeedMultiplier
        {
            get => config.moveSpeedMultiplier;
            set => config.moveSpeedMultiplier = value;
        }

        [UIValue("adjustVerticalForceToggle")]
        private bool adjustVerticalForceToggle
        {
            get => config.adjustVerticalForceToggle;
            set => config.adjustVerticalForceToggle = value;
        }

        [UIValue("cutDirMultiplier")]
        private float cutDirMultiplier
        {
            get => config.cutDirMultiplier;
            set => config.cutDirMultiplier = value;
        }

        [UIValue("fromCenterSpeed")]
        private float fromCenterSpeed
        {
            get => config.fromCenterSpeed;
            set => config.fromCenterSpeed = value;
        }

        [UIValue("rotation")]
        private float rotation
        {
            get => config.rotation;
            set => config.rotation = value;
        }


        [UIValue("randomRotationToggle")]
        private bool randomRotationToggle
        {
            get => config.randomRotationToggle;
            set => config.randomRotationToggle = value;
        }

        [UIValue("randomRotation")]
        private float randomRotation
        {
            get => config.randomRotation;
            set => config.randomRotation = value;
        }

        [UIValue("randomCutFromCenter")]
        private float randomCutFromCenter
        {
            get => config.randomCutFromCenter;
            set => config.randomCutFromCenter = value;
        }

        [UIValue("dynamicDebrisToggle")]
        private bool dynamicDebrisToggle
        {
            get => config.dynamicDebrisToggle;
            set => config.dynamicDebrisToggle = value;
        }

        [UIValue("saberSens")]
        private float saberSens
        {
            get => config.saberSens;
            set => config.saberSens = value;
        }



        //reset value garabage
        [UIAction("ResetMoveSpeedMultiplier")]
        private void ResetMoveSpeedMultiplier()
        {
            moveSpeedMultiplier = 0.5f;
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }

        [UIAction("ResetCutDirMultiplier")]
        private void ResetCutDirMultiplier()
        {
            cutDirMultiplier = 1.2f;
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }

        [UIAction("ResetFromCenterSpeed")]
        private void ResetFromCenterSpeed()
        {
            fromCenterSpeed = 4f;
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }

        [UIAction("resetRotation")]
        private void resetRotation()
        {
            rotation = 4f;
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }
        // profile stuff
        [UIAction("loadclicked")]
        private void LoadProfile()
        {
            int idx = (int)config.profile_value;
            config.LoadProfileLeft(idx);
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");
        }

        public void RefreshUI(string BSMLLocation)
        {
            Plugin.Log.Info("Refresh Successful");
            foreach (var child in gameObject.transform.Cast<Transform>().ToList())
            {
                GameObject.Destroy(child.gameObject);
            }

            BSMLParser.Instance.Parse(Utilities.GetResourceContent(
            Assembly.GetExecutingAssembly(), BSMLLocation), this.gameObject, this);
        }

    }

    public static class BsmlWrapper
    {
        public static void EnableUI()
        {
            DTFlow.Initialise();
        }
        public static void DisableUI()
        {
            DTFlow.Deinit();
        }
    }
}
