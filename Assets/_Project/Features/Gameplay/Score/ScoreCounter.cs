using _Project.Core.Data;

namespace _Project.Features.Gameplay.Score
{
    public class ScoreCounter
    {
        private readonly PlayerModel _playerModel;

        public ScoreCounter(PlayerModel playerModel)
        {
            _playerModel = playerModel;
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