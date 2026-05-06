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
        private readonly ChunkSpawner _chunkSpawner;
        private readonly IConfigProvider _configProvider;
        private readonly SignalBus _signalBus;


        public ChunkTeleporter(ChunkSpawner chunkSpawner, IConfigProvider configProvider, SignalBus signalBus)
        {
            _chunkSpawner = chunkSpawner;
            _configProvider = configProvider;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _chunkConfig = _configProvider.GetConfig<ChunkConfig>("ChunkConfig");
            _firstChunk = _chunkSpawner.Chunks.First.Value;
            _signalBus.Subscribe<ChunkTeleportedSignal>(OnChunkTeleported);
        }

        private void OnChunkTeleported()
        {
            _firstChunk = _chunkSpawner.Chunks.First.Value;
        }

        public void Tick()
        {
            CheckFirstChunk();
        }
        
        private void CheckFirstChunk()
        {
            var pos =  _firstChunk.transform.localPosition;
            if (pos.x < _chunkConfig.teleportationPositionX)
            {
                _signalBus.Fire<ChunkInTeleportZoneSignal>(new ChunkInTeleportZoneSignal(_firstChunk));
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ChunkTeleportedSignal>(OnChunkTeleported);
        }
    }
}
