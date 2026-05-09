using System;
using _Project.Core.Signals;
using _Project.Core.Tools;
using Zenject;


namespace _Project.Core.Infrastructure
{
    public class SceneLoader : IInitializable, IDisposable
    {
        private const string GameplaySceneName = "Game";
        private const string MainMenuSceneName = "MainMenu";
        
        private readonly SceneLoadService _sceneLoadService;
        private readonly SignalBus _signalBus;
        
        
        public SceneLoader(SceneLoadService sceneLoadService, SignalBus signalBus)
        {
            _sceneLoadService = sceneLoadService;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartGameClickedSignal>(LoadGameScene);
            _signalBus.Subscribe<MenuClickedSignal>(LoadMenuScene);
        }

        public void LoadGameScene()
        {
            _sceneLoadService.LoadScene(GameplaySceneName);
        }

        public void LoadMenuScene()
        {
            _sceneLoadService.LoadScene(MainMenuSceneName);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameClickedSignal>(LoadGameScene);
            _signalBus.Unsubscribe<MenuClickedSignal>(LoadMenuScene);
        }
    }
}