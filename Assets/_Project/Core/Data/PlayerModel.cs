using System;
using _Project.Core.Infrastructure.Save;
using _Project.Core.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Core.Data
{
    public class PlayerModel : IInitializable, IDisposable
    {
        public event Action<int> OnCurrentScoreChanged;
        public event Action<int> OnCoinsChanged;
        private int _currentScore;
        private PlayerProgress _playerProgress;
        private readonly SignalBus _signalBus;
        private ISaveService _saveService;
        
        public int MaxScore => _playerProgress.maxScore;
        public int Coins
        {
            get => _playerProgress.coins;
            set
            {
                if (value != _playerProgress.coins)
                {
                    _playerProgress.coins = value;
                    OnCoinsChanged?.Invoke(value);
                }
            }
        }
        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                if (value != _currentScore)
                {
                    _currentScore = value;
                    OnCurrentScoreChanged?.Invoke(value);
                }
            }
        }
        public PlayerProgress PlayerProgress => _playerProgress;


        public PlayerModel(ISaveService saveService, SignalBus signalBus)
        {
            _saveService = saveService;
            _signalBus = signalBus;
        }
        
        public void TryUpdateMaxScore(int newMaxScore)
        {
            if (newMaxScore > MaxScore)
            {
                _playerProgress.maxScore = newMaxScore;
            }
        }

        public void IncreaseCurrentScore()
        {
            CurrentScore++;
        }

        public void ResetCurrentScore()
        {
            CurrentScore = 0;
        }

        public void AddGold(int amount)
        {
            Coins += amount;
        }

        public bool TryRemoveGold(int amount)
        {
            if (Coins >= amount)
            {
                Coins -= amount;
                return true;
            }
            return false;
        }

        public void Initialize()
        {
            _playerProgress = _saveService.Load<PlayerProgress>() ?? new PlayerProgress();
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<GameRestartedSignal>(ResetCurrentScore);
            ResetCurrentScore();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _signalBus.Unsubscribe<GameRestartedSignal>(ResetCurrentScore);
        }

        private void OnGameOver()
        {
            TryUpdateMaxScore(CurrentScore);
            _saveService.Save(PlayerProgress);
        }
    }
}