using _Project.Core.Data;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Infrastructure.Save;
using _Project.Core.Input;
using _Project.Core.States;
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
            BindGameStateMachine();
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
                .To<PlayerPrefsSaveService>()
                .AsSingle();
        }
        
        private void BindConfigProvider()
        {
            Container
                .Bind<IConfigProvider>()
                .To<ResourcesConfigProvider>()
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

        private void BindGameStateMachine()
        {
            
            Container
                .Bind<IGameState>()
                .To<MenuState>()
                .AsSingle();
            Container
                .Bind<IGameState>()
                .To<GameplayState>()
                .AsSingle();
            Container
                .Bind<IGameState>()
                .To<GameOverState>()
                .AsSingle();
            Container
                .Bind<GameStateMachine>()
                .AsSingle();
        }
    }
}