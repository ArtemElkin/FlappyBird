using System;
using System.Collections.Generic;
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
        private PlayerSave _playerSave;
        private readonly SignalBus _signalBus;
        private readonly ISaveService _saveService;
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
        public List<int> UnlockedBackgroundIds => _playerSave.unlockedBackgroundIds;


        public PlayerModel(ISaveService saveService, SignalBus signalBus)
        {
            _saveService = saveService;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<GameRestartedSignal>(ResetCurrentScore);
            
            Load();
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
            _playerSave.unlockedBackgroundIds.Add(backgroundId);
        }

        public void Save()
        {
            _saveService.Save(_playerSave);
        }
        private void Load()
        {
            _playerSave = _saveService.Load<PlayerSave>() ?? new PlayerSave();
        }

        private void OnGameOver()
        {
            TryUpdateMaxScore(CurrentScore);
            Save();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _signalBus.Unsubscribe<GameRestartedSignal>(ResetCurrentScore);
        }
    }
}