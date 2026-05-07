using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkTeleporter : IInitializable, IDisposable, ITickable
    {

        private ChunkComponent _firstChunk;
        private ChunkConfig _chunkConfig;
        private readonly IConfigProvider _configProvider;
        private readonly SignalBus _signalBus;


        public ChunkTeleporter(ChunkSpawner chunkSpawner, IConfigProvider configProvider, SignalBus signalBus)
        {
            _configProvider = configProvider;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _chunkConfig = _configProvider.GetConfig<ChunkConfig>("ChunkConfig");
            _signalBus.Subscribe<FirstChunkChangedSignal>(OnChunkTeleported);
            // _signalBus.Subscribe<>
        }

        private void OnChunkTeleported(FirstChunkChangedSignal signal)
        {
            _firstChunk = signal.newFirstChunk;
        }

        public void Tick()
        {
            CheckFirstChunk();
        }
        
        private void CheckFirstChunk()
        {
            if (_firstChunk == null) return;
            var pos =  _firstChunk.transform.localPosition;
            if (pos.x < _chunkConfig.teleportationPositionX)
            {
                _signalBus.Fire<ChunkInTeleportZoneSignal>(new ChunkInTeleportZoneSignal(_firstChunk));
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FirstChunkChangedSignal>(OnChunkTeleported);
        }
    }
}
