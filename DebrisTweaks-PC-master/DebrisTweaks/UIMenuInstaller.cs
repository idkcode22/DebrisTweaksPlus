using DebrisTweaks.OnlineUI;
using JetBrains.Annotations;
using Zenject;

namespace DebrisTweaksMenuInstall
{
    [UsedImplicitly]
    public class DebrisTweaksMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplaySetupPanel>().AsSingle();
        }
    }
}