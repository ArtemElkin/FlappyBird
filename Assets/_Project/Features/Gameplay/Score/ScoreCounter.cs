using System;
using _Project.Core.Data;
using _Project.Core.Signals;
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
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<GameRestartedSignal>(OnGameRestarted);
            
            _playerModel.ResetCurrentScore();
        }
        
        public void IncreaseCurrentScore()
        {
            _playerModel.IncreaseCurrentScore();
            
        }

        private void OnGameOver()
        {
            _playerModel.TryUpdateMaxScore(_playerModel.CurrentScore);
            _playerModel.Save();
            
        }

        private void OnGameRestarted()
        {
            _playerModel.ResetCurrentScore();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GapPassedSignal>(IncreaseCurrentScore);
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _signalBus.Unsubscribe<GameRestartedSignal>(OnGameRestarted);
        }
    }
}