using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using _Project.Features.Gameplay.Chunk;
using _Project.Features.Gameplay.Signals;
using Zenject;

namespace _Project.Features.Gameplay.Background
{
    public class BackgroundWarper : IInitializable, IDisposable, ITickable
    {
        private float _warpPositionX;
        private List<BackgroundComponent> _firstBackgrounds;
        private readonly SignalBus _signalBus;
        private readonly IConfigProvider _configProvider;
        
        
        public BackgroundWarper(
            SignalBus signalBus,
            IConfigProvider configProvider)
        {
            _signalBus = signalBus;
            _configProvider = configProvider;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<FirstBackgroundChangedSignal>(OnFirstBackgroundChanged);
            var config = _configProvider.GetConfig<ChunkConfig>("ChunkConfig");
            _warpPositionX = config.warpPositionX;
        }

        public void Setup(List<BackgroundComponent> firstBackgrounds)
        {
            _firstBackgrounds = firstBackgrounds;
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
                if (pos.x < _warpPositionX)
                {
                    _signalBus.Fire<BackgroundInWarpZoneSignal>(new BackgroundInWarpZoneSignal(_firstBackgrounds[i]));
                }
            }
        }

        private void OnFirstBackgroundChanged(FirstBackgroundChangedSignal signal)
        {
            _firstBackgrounds.Remove(signal.previousFirstBackground);
            _firstBackgrounds.Add(signal.newFirstBackground);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FirstBackgroundChangedSignal>(OnFirstBackgroundChanged);;
        }
    }
}