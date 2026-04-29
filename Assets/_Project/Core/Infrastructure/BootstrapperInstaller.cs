using _Project.Core.Input;
using UnityEngine;
using Zenject;

namespace _Project.Core.Infrastructure
{
    public class BootstrapperInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _inputHandlerPrefab;
        
        public override void InstallBindings()
        {
            BindInput();
        }
        private void BindInput()
        {
            Container
                .Bind<IInputService>()
                .To<InputHandler>()
                .FromComponentInNewPrefab(_inputHandlerPrefab)
                .AsSingle();
        }

        
    }
}