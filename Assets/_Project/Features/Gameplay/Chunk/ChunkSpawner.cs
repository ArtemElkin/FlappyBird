using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Chunk.PipePair;
using _Project.Features.Gameplay.Coin;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkSpawner : IInitializable, IDisposable
    {
        private LinkedList<ChunkComponent> _chunks;
        private ChunkConfig _config;
        private float _chunkWidth;
        private readonly IConfigProvider _configProvider;
        private readonly PipePairFactory _pipePairFactory;
        private readonly CoinFactory _coinFactory;
        private readonly SignalBus _signalBus;
        private readonly ChunkWarper _chunkWarper;
        private readonly ScreenBoundsCalculator _screenBoundsCalculator;
        private readonly ChunkBuilder _chunkBuilder;
        private readonly ChunkFactory _chunkFactory;


        public ChunkSpawner(
            IConfigProvider configProvider,
            CoinFactory coinFactory,
            SignalBus signalBus,
            ScreenBoundsCalculator screenBoundsCalculator,
            ChunkBuilder chunkBuilder,
            ChunkFactory chunkFactory,
            PipePairFactory pipePairFactory,
            ChunkWarper chunkWarper)
        {
            _configProvider = configProvider;
            _coinFactory = coinFactory;
            _signalBus = signalBus;
            _screenBoundsCalculator = screenBoundsCalculator;
            _chunkBuilder = chunkBuilder;
            _chunkFactory = chunkFactory;
            _pipePairFactory = pipePairFactory;
            _chunkWarper = chunkWarper;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<ChunkInWarpZoneSignal>(OnChunkInWarpZone);
            _signalBus.Subscribe<GameRestartedSignal>(OnGameRestarted);
            
            _config = _configProvider.GetConfigFromJson<ChunkConfig>("ChunkConfig");

            _chunkBuilder
                .SetSpeed(_config.moveSpeed)
                .SetElementsPerChunk(_config.elementsPerChunk)
                .SetIntervalBetweenElements(_config.intervalBetweenElements);
            _chunks = new LinkedList<ChunkComponent>();
            _chunkWidth = _config.elementsPerChunk * _config.intervalBetweenElements;
            SpawnChunks();
            _chunkWarper.Setup(_chunks.First.Value, _chunkWidth);
        }
        
        private void SpawnChunks()
        {
            for (int i = 0; i < _config.chunksCount; i++)
            {
                var chunkLocalPosX = i * _chunkWidth + _screenBoundsCalculator.RightEdgeX + _chunkWidth / 2;
                var chunk = _chunkBuilder
                    .SetInitialPosition(new Vector3(chunkLocalPosX, 0f, 0f))
                    .AddPipePairs(_config.maxElementOffsetY)
                    .AddCoins(_config.maxElementOffsetY)
                    .Build();
                
                _chunks.AddLast(chunk);
            }
        }
        
        private void OnChunkInWarpZone(ChunkInWarpZoneSignal signal)
        {
            var chunk = signal.chunkToWarp;
            var lastChunkPos = _chunks.Last.Value.transform.localPosition;
            var newPosX = lastChunkPos.x + _config.intervalBetweenElements * _config.elementsPerChunk;
            var newPos = new Vector3(newPosX, lastChunkPos.y, lastChunkPos.z);
            _chunks.RemoveFirst();
            DespawnChunk(chunk);
            var newChunk = _chunkBuilder
                .SetInitialPosition(newPos)
                .AddPipePairs(_config.maxElementOffsetY)
                .AddCoins(_config.maxElementOffsetY)
                .Build();
            _chunks.AddLast(newChunk);
            
            _signalBus.Fire<FirstChunkChangedSignal>(new FirstChunkChangedSignal(newFirstChunk: _chunks.First.Value));
        }
        
        private void DespawnChunk(ChunkComponent chunk)
        {
            foreach (var coin in chunk.Coins)
            {
                _coinFactory.Release(coin);
            }
            chunk.Coins.Clear();

            foreach (var pipePair in chunk.PipePairs)
            {
                _pipePairFactory.Release(pipePair);
            }
            chunk.PipePairs.Clear();
            _chunkFactory.Release(chunk);
        }

        private void OnGameRestarted()
        {
            foreach (var chunk in _chunks)
            {
                DespawnChunk(chunk);
            }
            _chunks.Clear();
            SpawnChunks();
            _signalBus.Fire<FirstChunkChangedSignal>(new FirstChunkChangedSignal(newFirstChunk: _chunks.First.Value));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ChunkInWarpZoneSignal>(OnChunkInWarpZone);
            _signalBus.Unsubscribe<GameRestartedSignal>(OnGameRestarted);
        }
    }
}