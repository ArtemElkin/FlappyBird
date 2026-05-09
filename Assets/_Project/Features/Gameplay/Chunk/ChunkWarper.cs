using System;
using _Project.Core.Infrastructure.Config;
using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkWarper : IInitializable, IDisposable, ITickable
    {
        private ChunkComponent _firstChunk;
        private ChunkConfig _chunkConfig;
        private readonly IConfigProvider _configProvider;
        private readonly SignalBus _signalBus;


        public ChunkWarper(IConfigProvider configProvider, SignalBus signalBus)
        {
            _configProvider = configProvider;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<FirstChunkChangedSignal>(OnChunkWarped);
            _chunkConfig = _configProvider.GetConfigFromJson<ChunkConfig>("ChunkConfig");
        }

        public void Setup(ChunkComponent firstChunk)
        {
            _firstChunk = firstChunk;
        }

        public void Tick()
        {
            CheckFirstChunk();
        }

        private void OnChunkWarped(FirstChunkChangedSignal signal)
        {
            _firstChunk = signal.newFirstChunk;
        }

        private void CheckFirstChunk()
        {
            if (_firstChunk == null) return;
            var pos =  _firstChunk.transform.localPosition;
            if (pos.x < _chunkConfig.warpPositionX)
            {
                _signalBus.Fire<ChunkInWarpZoneSignal>(new ChunkInWarpZoneSignal(_firstChunk));
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FirstChunkChangedSignal>(OnChunkWarped);
        }
    }
}
