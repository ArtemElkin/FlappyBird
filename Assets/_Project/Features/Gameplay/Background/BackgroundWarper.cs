using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Chunk;
using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.Background
{
    public class BackgroundWarper : IInitializable, IDisposable, ITickable
    {
        private float _warpPosX;
        private List<BackgroundLayerComponent> _firstBackgrounds;
        private readonly SignalBus _signalBus;
        private readonly IConfigProvider _configProvider;
        private readonly ScreenBoundsCalculator _screenBoundsCalculator;
        
        
        public BackgroundWarper(
            SignalBus signalBus,
            ScreenBoundsCalculator screenBoundsCalculator)
        {
            _signalBus = signalBus;
            _screenBoundsCalculator = screenBoundsCalculator;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<FirstBackgroundChangedSignal>(OnFirstBackgroundChanged);
        }

        public void Setup(List<BackgroundLayerComponent> firstBackgrounds, float warpPosX)
        {
            _firstBackgrounds = firstBackgrounds;
            _warpPosX = warpPosX;
        }

        public void Tick()
        {
            CheckFirstBackgrounds();
        }

        private void CheckFirstBackgrounds()
        {
            for (int i = _firstBackgrounds.Count - 1; i >= 0; i--)
            {
                var pos = _firstBackgrounds[i].transform.localPosition;
                if (pos.x < _warpPosX)
                {
                    _signalBus.Fire<BackgroundInWarpZoneSignal>(new BackgroundInWarpZoneSignal(_firstBackgrounds[i]));
                }
            }
        }

        private void OnFirstBackgroundChanged(FirstBackgroundChangedSignal signal)
        {
            _firstBackgrounds.Remove(signal.PreviousFirstBackgroundLayer);
            _firstBackgrounds.Add(signal.NewFirstBackgroundLayer);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FirstBackgroundChangedSignal>(OnFirstBackgroundChanged);;
        }
    }
}