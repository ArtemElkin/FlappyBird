using System;
using _Project.Core.Ads;
using _Project.Core.Signals;
using Zenject;

namespace _Project.Features.Gameplay.Ads
{
    public class GameplayAdsController : IInitializable, IDisposable
    {
        private const int DeathsPerAdInterval = 3;
        private int _deathsFromLastAd;
        private SignalBus _signalBus;
        private IAdsService _adsService;

        public GameplayAdsController(SignalBus signalBus, IAdsService adsService)
        {
            _signalBus = signalBus;
            _adsService = adsService;
        }


        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<GameRestartedSignal>(OnGameRestarted);
            _signalBus.Subscribe<MenuClickedSignal>(OnMenuClicked);

            _deathsFromLastAd = 0;
        }

        private void OnGameOver()
        {
            _adsService.ShowBanner();
            _deathsFromLastAd++;
            if (_deathsFromLastAd >= DeathsPerAdInterval)
            {
                _adsService.ShowInterstitial();
                _deathsFromLastAd = 0;
            }
        }

        private void OnGameRestarted()
        {
            _adsService.HideBanner();
        }

        private void OnMenuClicked()
        {
            _adsService.HideBanner();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _signalBus.Unsubscribe<GameRestartedSignal>(OnGameRestarted);
            _signalBus.Unsubscribe<MenuClickedSignal>(OnMenuClicked);
        }
    }
}