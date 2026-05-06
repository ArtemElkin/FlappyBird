using _Project.Core.Data;
using _Project.Core.Infrastructure.Save;
using _Project.Core.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Core.States
{
    public class GameOverState : IGameState
    {
        private readonly PlayerModel _playerModel;
        private readonly ISaveService _saveService;
        private readonly PlayerProgress _playerProgress;
        private readonly SignalBus _signalBus;
        public GameOverState(PlayerModel playerModel, ISaveService saveService, SignalBus signalBus)
        {
            _playerModel = playerModel;
            _saveService = saveService;
            _signalBus = signalBus;
        }
        public void Enter()
        {
            _playerModel.TryUpdateMaxScore(_playerModel.CurrentScore);
            var progress = _playerModel.PlayerProgress;
            _saveService.Save(progress);
            
            _signalBus.Fire<GameOverSignal>();
        }

        public void Exit()
        {
        }
    }
}