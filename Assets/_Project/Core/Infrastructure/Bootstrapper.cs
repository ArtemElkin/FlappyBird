using _Project.Core.Data;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Infrastructure.Save;
using Zenject;

namespace _Project.Core.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly IConfigProvider _configProvider;
        private readonly ISaveService _saveService;
        private readonly PlayerModel _playerModel;
        private readonly SceneLoader _sceneLoader;
        
        public Bootstrapper(
            IConfigProvider configProvider,
            ISaveService saveService,
            PlayerModel playerModel,
            SceneLoader sceneLoader)
        {
            _configProvider = configProvider;
            _saveService = saveService;
            _playerModel = playerModel;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            // var save = _saveService.Load<PlayerProgress>() ?? new PlayerProgress();
            
            // _playerModel.Setup(save);

            // _gameStateMachine.EnterState<MenuState>();
            _sceneLoader.LoadMenuScene();
        }
    }
}
