using System;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Features.UI.Gameplay.Pause
{
    public class PauseController : IInitializable, IDisposable
    {
        private readonly GameObject _pausePanel;
        private readonly SignalBus _signalBus;
        
        public PauseController(GameObject pausePanel, SignalBus signalBus)
        {
            _pausePanel = pausePanel;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(ShowPausePanel);
            _signalBus.Subscribe<GameStartedSignal>(HidePausePanel);
        }
        
        private void ShowPausePanel()
        {
            _pausePanel.SetActive(true);
        }

        private void HidePausePanel()
        {
            _pausePanel.SetActive(false);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameOverSignal>(ShowPausePanel);
            _signalBus.Unsubscribe<GameStartedSignal>(HidePausePanel);
        }
    }
}