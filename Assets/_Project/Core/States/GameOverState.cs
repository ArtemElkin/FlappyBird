using _Project.Core.Data;
using _Project.Core.Infrastructure.Save;
using UnityEngine;

namespace _Project.Core.States
{
    public class GameOverState : IGameState
    {
        private readonly PlayerModel _playerModel;
        private readonly ISaveService _saveService;
        private readonly PlayerProgress _playerProgress;
        public GameOverState(PlayerModel playerModel, ISaveService saveService)
        {
            _playerModel = playerModel;
            _saveService = saveService;
        }
        public void Enter()
        {
            _playerModel.TryUpdateMaxScore(_playerModel.CurrentScore);
            var progress = _playerModel.PlayerProgress;
            _saveService.Save(progress);
            Debug.Log("Save Complete");
        }

        public void Exit()
        {
        }
    }
}