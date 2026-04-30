using System;


namespace _Project.Core.Data
{
    public class PlayerModel
    {
        public event Action<int> OnCurrentScoreChanged;
        public event Action<int> OnGoldChanged;
        private int _currentScore;
        private PlayerProgress _playerProgress;
        
        public int MaxScore => _playerProgress.maxScore;
        public int Gold
        {
            get => _playerProgress.gold;
            set
            {
                if (value != _playerProgress.gold)
                {
                    _playerProgress.gold = value;
                    OnGoldChanged?.Invoke(value);
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
        
        
        public void Setup(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            CurrentScore = 0;
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
            Gold += amount;
        }

        public bool TryRemoveGold(int amount)
        {
            if (_playerProgress.gold >= amount)
            {
                _playerProgress.gold -= amount;
                return true;
            }
            return false;
        }
    }
}