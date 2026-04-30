using _Project.Core.Infrastructure.Config;
using UnityEngine;
using Zenject;

namespace _Project.Core.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Биндинг лоад сервисов, sdk, сигнальной шины
            BindConfigProvider();
            BindSignalBus();
        }

        private void BindConfigProvider()
        {
            Container
                .Bind<IConfigProvider>()
                .To<JsonConfigProvider>()
                .AsSingle();
        }
        
        private void BindSignalBus()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}