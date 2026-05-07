using System;
using _Project.Core.Data;
using Zenject;

namespace _Project.Features.Gameplay.Score
{
    public class ScoreCounter : IInitializable
    {
        private readonly PlayerModel _playerModel;

        public ScoreCounter(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            _playerModel.ResetCurrentScore();
        }
        
        public void IncreaseCurrentScore()
        {
            _playerModel.IncreaseCurrentScore();
            if (_playerModel.CurrentScore > _playerModel.MaxScore)
            {
                _playerModel.TryUpdateMaxScore(_playerModel.CurrentScore);
            }
        }
    }
}