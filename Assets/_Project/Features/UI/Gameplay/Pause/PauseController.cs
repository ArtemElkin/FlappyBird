using System;
using _Project.Core.Ads;
using _Project.Core.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.UI.Gameplay.Pause
{
    public class PauseController : IInitializable, IDisposable
    {
        private readonly GameObject _pausePanel;
        private readonly SignalBus _signalBus;
        private readonly IAdsService _adsService;
        
        
        public PauseController(GameObject pausePanel, SignalBus signalBus, IAdsService  adsService)
        {
            _pausePanel = pausePanel;
            _signalBus = signalBus;
            _adsService = adsService;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(ShowPausePanel);
            _signalBus.Subscribe<GameRestartedSignal>(HidePausePanel);
        }
        
        private void ShowPausePanel()
        {
            _pausePanel.SetActive(true);
            _adsService.ShowBanner();
        }

        private void HidePausePanel()
        {
            _pausePanel.SetActive(false);
            _adsService.HideBanner();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameOverSignal>(ShowPausePanel);
            _signalBus.Unsubscribe<GameRestartedSignal>(HidePausePanel);
        }
    }
}