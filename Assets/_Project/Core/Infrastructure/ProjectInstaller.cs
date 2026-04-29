using UnityEngine;
using Zenject;

namespace _Project.Core.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Биндинг лоад сервисов, sdk, сигнальной шины
            BindSignalBus();
        }
        
        private void BindSignalBus()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}