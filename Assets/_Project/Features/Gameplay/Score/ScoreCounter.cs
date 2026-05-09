using System;
using _Project.Core.Data;
using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.Score
{
    public class ScoreCounter : IInitializable, IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly SignalBus _signalBus;

        
        public ScoreCounter(PlayerModel playerModel, SignalBus signalBus)
        {
            _playerModel = playerModel;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GapPassedSignal>(IncreaseCurrentScore);
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

        public void Dispose()
        {
            _signalBus.Unsubscribe<GapPassedSignal>(IncreaseCurrentScore);
        }
    }
}