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

        [UIValue("VelocityMult")]
        private float forceMultiplier
        {
            get => config.forceMultiplier;
            set => config.forceMultiplier = value;
        }


        [UIValue("DragMult")]
        private float DragMultiplier
        {
            get => config.DragMultiplier;
            set => config.DragMultiplier = value;
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
        //wouldve made it where it loads on all screen with one click but trying to figure out was making me want to take whippets
        private void LoadProfile()
        {

            if (profile_value == 1f)
            {
                config.ModToggle = config.ModToggle1;
                config.forceMultiplier = config.forceMultiplier1;
                config.DragMultiplier = config.DragMultiplier1;
                config.GravityToggle = config.GravityToggle1;
                config.RotationToggle = config.RotationToggle1;
            }

            if (profile_value == 2f)
            {
                config.ModToggle = config.ModToggle2;
                config.forceMultiplier = config.forceMultiplier2;
                config.DragMultiplier = config.DragMultiplier2;
                config.GravityToggle = config.GravityToggle2;
                config.RotationToggle = config.RotationToggle2;
            }

            if (profile_value == 3f)
            {
                config.ModToggle = config.ModToggle3;
                config.forceMultiplier = config.forceMultiplier3;
                config.DragMultiplier = config.DragMultiplier3;
                config.GravityToggle = config.GravityToggle3;
                config.RotationToggle = config.RotationToggle3;
            }

            if (profile_value == 4f)
            {
                config.ModToggle = config.ModToggle4;
                config.forceMultiplier = config.forceMultiplier4;
                config.DragMultiplier = config.DragMultiplier4;
                config.GravityToggle = config.GravityToggle4;
                config.RotationToggle = config.RotationToggle4;
            }

            if (profile_value == 5f)
            {
                config.ModToggle = config.ModToggle5;
                config.forceMultiplier = config.forceMultiplier5;
                config.DragMultiplier = config.DragMultiplier5;
                config.GravityToggle = config.GravityToggle5;
                config.RotationToggle = config.RotationToggle5;
            }
            //var fent = new DTSideView();
            //var fent2 = new DTLeftSideView();
            //DTFlow.LoadProfile(profile_value);
            //fent.LoadProfile(profile_value);
            //fent2.LoadProfile(profile_value);
            RefreshUI("DebrisTweaks.UI.MainView.bsml");
            NotifyPropertyChanged(nameof(ModToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(forceMultiplier));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(DragMultiplier));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(GravityToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(RotationToggle));  // Notify the UI of the value change
        }

        [UIAction("saveclicked")]
        private void SaveProfile()
        {



            if (profile_value == 1f)
            {
                config.ModToggle1 = config.ModToggle;
                config.forceMultiplier1 = config.forceMultiplier;
                config.DragMultiplier1 = config.DragMultiplier;
                config.GravityToggle1 = config.GravityToggle;
                config.RotationToggle1 = config.RotationToggle;
            }

            if (profile_value == 2f)
            {
                config.ModToggle2 = config.ModToggle;
                config.forceMultiplier2 = config.forceMultiplier;
                config.DragMultiplier2 = config.DragMultiplier;
                config.GravityToggle2 = config.GravityToggle;
                config.RotationToggle2 = config.RotationToggle;
            }

            if (profile_value == 3f)
            {
                config.ModToggle3 = config.ModToggle;
                config.forceMultiplier3 = config.forceMultiplier;
                config.DragMultiplier3 = config.DragMultiplier;
                config.GravityToggle3 = config.GravityToggle;
                config.RotationToggle3 = config.RotationToggle;
            }

            if (profile_value == 4f)
            {
                config.ModToggle4 = config.ModToggle;
                config.forceMultiplier4 = config.forceMultiplier;
                config.DragMultiplier4 = config.DragMultiplier;
                config.GravityToggle4 = config.GravityToggle;
                config.RotationToggle4 = config.RotationToggle;
            }

            if (profile_value == 5f)
            {
                config.ModToggle5 = config.ModToggle;
                config.forceMultiplier5 = config.forceMultiplier;
                config.DragMultiplier5 = config.DragMultiplier;
                config.GravityToggle5 = config.GravityToggle;
                config.RotationToggle5 = config.RotationToggle;
            }
           // RefreshUI("DebrisTweaks.UI.MainView.bsml");
            var fent = new DTSideView();
            var fent2 = new DTLeftSideView();
            fent.SaveProfile(profile_value);
            fent2.SaveProfile(profile_value);
            NotifyPropertyChanged(nameof(ModToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(forceMultiplier));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(DragMultiplier));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(GravityToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(RotationToggle));  // Notify the UI of the value change
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
            NotifyPropertyChanged(nameof(minLifetime));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.SideView.bsml");

        }
        [UIAction("resetMaxLifeTime")]
        private void resetMaxLifeTime()
        {
            maxLifetime = 2f;
            NotifyPropertyChanged(nameof(maxLifetime));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.SideView.bsml");

        }
        [UIAction("resetLifeTimeOffset")]
        private void resetLifeTimeOffset()
        {
            lifeTimeOffset = 0.05f;
            NotifyPropertyChanged(nameof(lifeTimeOffset));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.SideView.bsml");

        }

        // profile stuff
        [UIAction("loadclicked")]
        private void LoadProfile()
        {
            var fent = new DTMainView();
            float Val = fent.profile_value;
            Plugin.Log.Info("Load profile from sideView success");

            if (Val == 1)
            {
                config.CustomColourToggle = config.CustomColourToggle1;
                config.LeftColour = config.LeftColour1;
                config.RightColour = config.RightColour1;
                config.minLifetime = config.minLifetime1;
                config.maxLifetime = config.maxLifetime1;
                config.lifeTimeOffset = config.lifeTimeOffset1;
                config.DebrisScale = config.DebrisScale1;
            }

            if (Val == 2)
            {
                config.CustomColourToggle = config.CustomColourToggle2;
                config.LeftColour = config.LeftColour2;
                config.RightColour = config.RightColour2;
                config.minLifetime = config.minLifetime2;
                config.maxLifetime = config.maxLifetime2;
                config.lifeTimeOffset = config.lifeTimeOffset2;
                config.DebrisScale = config.DebrisScale2;
            }

            if (Val == 3)
            {
                config.CustomColourToggle = config.CustomColourToggle3;
                config.LeftColour = config.LeftColour3;
                config.RightColour = config.RightColour3;
                config.minLifetime = config.minLifetime3;
                config.maxLifetime = config.maxLifetime3;
                config.lifeTimeOffset = config.lifeTimeOffset3;
                config.DebrisScale = config.DebrisScale3;
            }

            if (Val == 4)
            {
                config.CustomColourToggle = config.CustomColourToggle4;
                config.LeftColour = config.LeftColour4;
                config.RightColour = config.RightColour4;
                config.minLifetime = config.minLifetime4;
                config.maxLifetime = config.maxLifetime4;
                config.lifeTimeOffset = config.lifeTimeOffset4;
                config.DebrisScale = config.DebrisScale4;
            }

            if (Val == 5)
            {
                config.CustomColourToggle = config.CustomColourToggle5;
                config.LeftColour = config.LeftColour5;
                config.RightColour = config.RightColour5;
                config.minLifetime = config.minLifetime5;
                config.maxLifetime = config.maxLifetime5;
                config.lifeTimeOffset = config.lifeTimeOffset5;
                config.DebrisScale = config.DebrisScale5;
            }

            NotifyPropertyChanged(nameof(CustomColourToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(LeftColour));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(RightColour));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(minLifetime));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(maxLifetime));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(lifeTimeOffset));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(DebrisScale));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.SideView.bsml");

        }

        public void SaveProfile(float Val)
        {
            if (Val == 1)
            {
                config.CustomColourToggle1 = config.CustomColourToggle;
                config.LeftColour1 = config.LeftColour;
                config.RightColour1 = config.RightColour;
                config.minLifetime1 = config.minLifetime;
                config.maxLifetime1 = config.maxLifetime;
                config.lifeTimeOffset1 = config.lifeTimeOffset;
                config.DebrisScale1 = config.DebrisScale;
            }

            if (Val == 2)
            {
                config.CustomColourToggle2 = config.CustomColourToggle;
                config.LeftColour2 = config.LeftColour;
                config.RightColour2 = config.RightColour;
                config.minLifetime2 = config.minLifetime;
                config.maxLifetime2 = config.maxLifetime;
                config.lifeTimeOffset2 = config.lifeTimeOffset;
                config.DebrisScale2 = config.DebrisScale;
            }

            if (Val == 3)
            {
                config.CustomColourToggle3 = config.CustomColourToggle;
                config.LeftColour3 = config.LeftColour;
                config.RightColour3 = config.RightColour;
                config.minLifetime3 = config.minLifetime;
                config.maxLifetime3 = config.maxLifetime;
                config.lifeTimeOffset3 = config.lifeTimeOffset;
                config.DebrisScale3 = config.DebrisScale;
            }

            if (Val == 4)
            {
                config.CustomColourToggle4 = config.CustomColourToggle;
                config.LeftColour4 = config.LeftColour;
                config.RightColour4 = config.RightColour;
                config.minLifetime4 = config.minLifetime;
                config.maxLifetime4 = config.maxLifetime;
                config.lifeTimeOffset4 = config.lifeTimeOffset;
                config.DebrisScale4 = config.DebrisScale;
            }

            if (Val == 5)
            {
                config.CustomColourToggle5 = config.CustomColourToggle;
                config.LeftColour5 = config.LeftColour;
                config.RightColour5 = config.RightColour;
                config.minLifetime5 = config.minLifetime;
                config.maxLifetime5 = config.maxLifetime;
                config.lifeTimeOffset5 = config.lifeTimeOffset;
                config.DebrisScale5 = config.DebrisScale;
            }

            NotifyPropertyChanged(nameof(CustomColourToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(LeftColour));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(RightColour));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(minLifetime));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(maxLifetime));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(lifeTimeOffset));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(DebrisScale));  // Notify the UI of the value change
            //RefreshUI("DebrisTweaks.UI.SideView.bsml");

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
            NotifyPropertyChanged(nameof(moveSpeedMultiplier));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }

        [UIAction("ResetCutDirMultiplier")]
        private void ResetCutDirMultiplier()
        {
            cutDirMultiplier = 1.2f;
            NotifyPropertyChanged(nameof(cutDirMultiplier));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }

        [UIAction("ResetFromCenterSpeed")]
        private void ResetFromCenterSpeed()
        {
            fromCenterSpeed = 4f;
            NotifyPropertyChanged(nameof(fromCenterSpeed));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }

        [UIAction("resetRotation")]
        private void resetRotation()
        {
            rotation = 4f;
            NotifyPropertyChanged(nameof(rotation));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }
        // profile stuff
        [UIAction("loadclicked")]
        private void LoadProfile()
        {
            Plugin.Log.Info("Load profile from leftSideView success");
            var fent = new DTMainView();
            float Val = fent.profile_value;
            if (Val == 1)
            {
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

            if (Val == 2)
            {
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

            if (Val == 3)
            {
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

            if (Val == 4)
            {
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

            if (Val == 5)
            {
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
            NotifyPropertyChanged(nameof(moveSpeedMultiplier));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(adjustVerticalForceToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(cutDirMultiplier));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(fromCenterSpeed));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(rotation));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(randomRotationToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(randomCutFromCenter));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(randomRotation));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(dynamicDebrisToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(saberSens));  // Notify the UI of the value change
            RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");



        }

        public void SaveProfile(float Val)
        {
            if (Val == 1)
            {
                config.moveSpeedMultiplier1 = config.moveSpeedMultiplier;
                config.adjustVerticalForceToggle1 = config.adjustVerticalForceToggle;
                config.cutDirMultiplier1 = config.cutDirMultiplier;
                config.fromCenterSpeed1 = config.fromCenterSpeed;
                config.rotation1 = config.rotation;
                config.randomRotationToggle1 = config.randomRotationToggle;
                config.randomRotation1 = config.randomRotation;
                config.randomCutFromCenter1 = config.randomCutFromCenter;
                config.dynamicDebrisToggle1 = config.dynamicDebrisToggle;
                config.saberSens1 = config.saberSens;

            }

            if (Val == 2)
            {
                config.moveSpeedMultiplier2 = config.moveSpeedMultiplier;
                config.adjustVerticalForceToggle2 = config.adjustVerticalForceToggle;
                config.cutDirMultiplier2 = config.cutDirMultiplier;
                config.fromCenterSpeed2 = config.fromCenterSpeed;
                config.rotation2 = config.rotation;
                config.randomRotationToggle2 = config.randomRotationToggle;
                config.randomRotation2 = config.randomRotation;
                config.randomCutFromCenter2 = config.randomCutFromCenter;
                config.dynamicDebrisToggle2 = config.dynamicDebrisToggle;
                config.saberSens2 = config.saberSens;
            }

            if (Val == 3)
            {
                config.moveSpeedMultiplier3 = config.moveSpeedMultiplier;
                config.adjustVerticalForceToggle3 = config.adjustVerticalForceToggle;
                config.cutDirMultiplier3 = config.cutDirMultiplier;
                config.fromCenterSpeed3 = config.fromCenterSpeed;
                config.rotation3 = config.rotation;
                config.randomRotationToggle3 = config.randomRotationToggle;
                config.randomRotation3 = config.randomRotation;
                config.randomCutFromCenter3 = config.randomCutFromCenter;
                config.dynamicDebrisToggle3 = config.dynamicDebrisToggle;
                config.saberSens3 = config.saberSens;
            }

            if (Val == 4)
            {
                config.moveSpeedMultiplier4 = config.moveSpeedMultiplier;
                config.adjustVerticalForceToggle4 = config.adjustVerticalForceToggle;
                config.cutDirMultiplier4 = config.cutDirMultiplier;
                config.fromCenterSpeed4 = config.fromCenterSpeed;
                config.rotation4 = config.rotation;
                config.randomRotationToggle4 = config.randomRotationToggle;
                config.randomRotation4 = config.randomRotation;
                config.randomCutFromCenter4 = config.randomCutFromCenter;
                config.dynamicDebrisToggle4 = config.dynamicDebrisToggle;
                config.saberSens4 = config.saberSens;
            }

            if (Val == 5)
            {
                config.moveSpeedMultiplier5 = config.moveSpeedMultiplier;
                config.adjustVerticalForceToggle5 = config.adjustVerticalForceToggle;
                config.cutDirMultiplier5 = config.cutDirMultiplier;
                config.fromCenterSpeed5 = config.fromCenterSpeed;
                config.rotation5 = config.rotation;
                config.randomRotationToggle5 = config.randomRotationToggle;
                config.randomRotation5 = config.randomRotation;
                config.randomCutFromCenter5 = config.randomCutFromCenter;
                config.dynamicDebrisToggle5 = config.dynamicDebrisToggle;
                config.saberSens5 = config.saberSens;
            }

            NotifyPropertyChanged(nameof(moveSpeedMultiplier));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(adjustVerticalForceToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(cutDirMultiplier));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(fromCenterSpeed));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(rotation));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(randomRotationToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(randomCutFromCenter));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(randomRotation));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(dynamicDebrisToggle));  // Notify the UI of the value change
            NotifyPropertyChanged(nameof(saberSens));  // Notify the UI of the value change
            //RefreshUI("DebrisTweaks.UI.LeftSideView.bsml");

        }

        // Helper method to refresh existing UI elements
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
