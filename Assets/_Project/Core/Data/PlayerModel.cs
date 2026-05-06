using System;
using Zenject;


namespace _Project.Core.Data
{
    public class PlayerModel
    {
        public bool IsAlive { get; set; }
        public event Action<int> OnCurrentScoreChanged;
        public event Action<int> OnCoinsChanged;
        private int _currentScore;
        private PlayerProgress _playerProgress;
        private readonly SignalBus _signalBus;
        
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

        public void SetAlive(bool isAlive)
        {
            IsAlive = isAlive;
        }
        
        public void Setup(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            ResetCurrentScore();
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
    }
}