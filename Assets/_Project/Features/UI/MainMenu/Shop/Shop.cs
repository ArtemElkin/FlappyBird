using System;
using System.Collections.Generic;
using _Project.Core.Ads;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Infrastructure.Save;
using _Project.Core.Player;
using _Project.Features.Gameplay.Background;
using _Project.Features.UI.MainMenu.Shop.Buttons;
using _Project.Features.UI.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.UI.MainMenu.Shop
{
    public class Shop : IInitializable, IDisposable
    {
        private int _currentShownBackgroundId;
        private Dictionary<int, BackgroundConfig> _backgroundConfigs;
        private readonly PlayerModel _playerModel;
        private readonly BackgroundCardView _backgroundCardView;
        private readonly BuyChooseBackgroundButton _additionalButton;
        private readonly SignalBus _signalBus;
        private readonly IConfigProvider _configProvider;
        private readonly IAdsService _adsService;

        
        public Shop(
            BackgroundCardView backgroundCardView,
            BuyChooseBackgroundButton  additionalButton,
            PlayerModel playerModel,
            SignalBus signalBus,
            IConfigProvider configProvider,
            IAdsService adsService)
        {
            _backgroundCardView = backgroundCardView;
            _additionalButton = additionalButton;
            _playerModel = playerModel;
            _signalBus = signalBus;
            _configProvider = configProvider;
            _adsService = adsService;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<NextBackgroundButtonClickedSignal>(OnNextButtonClicked);
            _signalBus.Subscribe<PreviousBackgroundButtonClickedSignal>(OnPreviousButtonClicked);
            _signalBus.Subscribe<BuyChooseBackgroundButtonClickedSignal>(OnBuyChooseButtonClicked);
            _currentShownBackgroundId = 0;
            _backgroundConfigs = LoadBackgroundConfigs();
            UpdateCard();
        }

        private Dictionary<int, BackgroundConfig> LoadBackgroundConfigs()
        {
            _backgroundConfigs = new Dictionary<int, BackgroundConfig>();
            var configs =  _configProvider.GetConfigsFromScriptableObjects<BackgroundConfig>("Backgrounds");
            foreach (var config in configs)
            {
                _backgroundConfigs.TryAdd(config.id, config);
            }
            return _backgroundConfigs;
        }

        private void UpdateCard()
        {
            bool unlocked = _playerModel.UnlockedBackgroundIds.Contains(_currentShownBackgroundId);
            bool isBonus = _backgroundConfigs[_currentShownBackgroundId] is  BonusBackgroundConfig;
            _backgroundCardView.UpdateCard(_backgroundConfigs[_currentShownBackgroundId], unlocked);
            
            var textOnBtn = unlocked ? "Choose" : (isBonus ? "Watch Ad" : "Buy");
            
            _additionalButton.UpdateText(textOnBtn);
            _additionalButton.gameObject.SetActive(_currentShownBackgroundId != _playerModel.CurrentBackgroundId);
        }

        private void OnNextButtonClicked()
        {
            _currentShownBackgroundId++;
            if (_currentShownBackgroundId == _backgroundConfigs.Count)
            {
                _currentShownBackgroundId = 0;
            }
            UpdateCard();
        }

        private void OnPreviousButtonClicked()
        {
            _currentShownBackgroundId--;
            if (_currentShownBackgroundId == -1)
            {
                _currentShownBackgroundId = _backgroundConfigs.Count - 1;
            }
            UpdateCard();
        }

        private void OnBuyChooseButtonClicked()
        {
            if (_playerModel.UnlockedBackgroundIds.Contains(_currentShownBackgroundId))
            {
                ChooseCurrentBackground();
            }
            else if (_backgroundConfigs[_currentShownBackgroundId] is BonusBackgroundConfig)
            {
                WatchAdToUnlockBackground();
            }
            else
            {
                BuyCurrentBackground();
            }
        }
        
        private void ChooseCurrentBackground()
        {
            _playerModel.SetCurrentBackgroundId(_currentShownBackgroundId);
            UpdateCard();
        }

        private void BuyCurrentBackground()
        {
            var price = _backgroundConfigs[_currentShownBackgroundId].price;
            if (_playerModel.TryRemoveCoins(price))
            {
                _playerModel.UnlockBackground(_currentShownBackgroundId);
            }
            UpdateCard();
        }

        private void WatchAdToUnlockBackground()
        {
            _adsService.ShowRewarded(() =>
            {
                _playerModel.UnlockBackground(_currentShownBackgroundId);
                _playerModel.Save();
                UpdateCard();
            });
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<NextBackgroundButtonClickedSignal>(OnNextButtonClicked);
            _signalBus.Unsubscribe<PreviousBackgroundButtonClickedSignal>(OnPreviousButtonClicked);
            _signalBus.Unsubscribe<BuyChooseBackgroundButtonClickedSignal>(OnBuyChooseButtonClicked);
        }
    }
}