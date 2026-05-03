using _Project.Core.Data;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Infrastructure.Save;
using _Project.Core.Input;
using UnityEngine;
using Zenject;

namespace _Project.Core.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _inputHandlerPrefab;
        public override void InstallBindings()
        {
            // Биндинг лоад сервисов, sdk, сигнальной шины
            BindSignalBus();
            BindPlayerModel();
            BindSaveService();
            BindConfigProvider();
            BindInput();
        }
        
        private void BindPlayerModel()
        {
            Container
                .Bind<PlayerModel>()
                .AsSingle();
        }

        private void BindSaveService()
        {
            Container
                .Bind<ISaveService>()
                .To<JsonSaveService>()
                .AsSingle();
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