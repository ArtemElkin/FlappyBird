using _Project.Core.Input;
using UnityEngine;
using Zenject;

namespace _Project.Core.Infrastructure
{
    public class BootstrapperInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootstrapper();
        }

        private void BindBootstrapper()
        {
            Container
                .BindInterfacesAndSelfTo<Bootstrapper>()
                .AsSingle();
        }
    }
}