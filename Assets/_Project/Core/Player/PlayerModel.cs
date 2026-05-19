using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Save;
using Zenject;


namespace _Project.Core.Player
{
    public class PlayerModel : IInitializable
    {
        public event Action<int> OnCurrentScoreChanged;
        public event Action<int> OnCoinsChanged;
        private int _currentScore;
        private PlayerSave _playerSave = new();
        public int MaxScore => _playerSave.maxScore;
        public int Coins =>  _playerSave.coins;
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
        public int CurrentBackgroundId => _playerSave.currentBackgroundId;
        public IReadOnlyList<int> UnlockedBackgroundIds => _playerSave.unlockedBackgroundIds;


        public void Initialize()
        {
            ResetCurrentScore();
        }

        public void IncreaseCurrentScore()
        {
            CurrentScore++;
        }

        public void ResetCurrentScore()
        {
            CurrentScore = 0;
        }

        public void TryUpdateMaxScore(int newMaxScore)
        {
            if (newMaxScore > MaxScore)
            {
                _playerSave.maxScore = newMaxScore;
            }
        }

        public void AddCoins(int amount)
        {
            _playerSave.coins += amount;
            OnCoinsChanged?.Invoke(_playerSave.coins);
        }

        public bool TryRemoveCoins(int amount)
        {
            if (Coins >= amount)
            {
                _playerSave.coins -= amount;
                OnCoinsChanged?.Invoke(_playerSave.coins);
                return true;
            }
            return false;
        }

        public void SetCurrentBackgroundId(int id)
        {
            _playerSave.currentBackgroundId = id;
        }

        public void UnlockBackground(int backgroundId)
        {
            if (!_playerSave.unlockedBackgroundIds.Contains(backgroundId))
            {
                _playerSave.unlockedBackgroundIds.Add(backgroundId);
            }
        }

        public PlayerSave GetSave() => _playerSave;

        public void SetSave(PlayerSave playerSave)
        {
            _playerSave = playerSave;
        }
    }
}