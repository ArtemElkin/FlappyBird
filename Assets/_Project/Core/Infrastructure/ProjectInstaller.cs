using _Project.Core.Ads;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Infrastructure.Save;
using _Project.Core.Input;
using _Project.Core.Player;
using _Project.Core.Signals;
using _Project.Core.Tools;
using UnityEngine;
using Zenject;


namespace _Project.Core.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _inputHandlerPrefab;
        
        
        public override void InstallBindings()
        {
            BindSignalBus();
            Container.DeclareSignal<GameOverSignal>();
            Container.DeclareSignal<GameRestartedSignal>();
            Container.DeclareSignal<StartGameClickedSignal>();
            Container.DeclareSignal<MenuClickedSignal>();
            
            BindSaveService();
            BindConfigProviders();
            BindPlayerModel();
            BindInput();
            BindSceneLoadService();
            BindSceneLoader();
            BindAdsService();
        }

        private void BindSignalBus()
        {
            SignalBusInstaller.Install(Container);
        }

        private void BindSaveService()
        {
            Container
                .Bind<ISaveService>()
                .To<PlayerPrefsSaveService>()
                .AsSingle();
        }

        private void BindConfigProviders()
        {
            Container
                .BindInterfacesAndSelfTo<ResourcesConfigProvider>()
                .AsSingle();
        }

        private void BindPlayerModel()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerModel>()
                .AsSingle();
        }

        private void BindInput()
        {
            Container
                .Bind<IInputService>()
                .To<InputHandler>()
                .FromComponentInNewPrefab(_inputHandlerPrefab)
                .AsSingle();
        }

        private void BindSceneLoadService()
        {
            Container
                .Bind<SceneLoadService>()
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle();
        }

        private void BindAdsService()
        {
#if UNITY_EDITOR
            Container
                .BindInterfacesAndSelfTo<MockAdsService>()
                .AsSingle();
#else
            Container
                .BindInterfacesAndSelfTo<YandexAdsService>()
                .AsSingle();
#endif
        }
    }
}