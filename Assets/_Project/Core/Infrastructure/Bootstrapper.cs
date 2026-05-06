using _Project.Core.Data;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Infrastructure.Save;
using _Project.Core.States;
using Zenject;

namespace _Project.Core.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly IConfigProvider _configProvider;
        private readonly ISaveService _saveService;
        private readonly PlayerModel _playerModel;
        private readonly GameStateMachine _gameStateMachine;
        
        public Bootstrapper(
            IConfigProvider configProvider,
            ISaveService saveService,
            PlayerModel playerModel,
            GameStateMachine gameStateMachine)
        {
            _configProvider = configProvider;
            _saveService = saveService;
            _playerModel = playerModel;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            var save = _saveService.Load<PlayerProgress>() ?? new PlayerProgress();
            
            _playerModel.Setup(save);

            _gameStateMachine.EnterState<MenuState>();
        }
    }
}
