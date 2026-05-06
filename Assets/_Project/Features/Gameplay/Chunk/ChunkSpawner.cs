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
        public LinkedList<ChunkComponent> Chunks => _chunks;
        private LinkedList<ChunkComponent> _chunks;
        private ChunkConfig _config;
        private CustomPool<ChunkComponent> _chunkPool;
        private readonly int _chunksCount = 4;
        private readonly Transform _parentPath;
        private readonly PipePairFactory _pipePairFactory;
        private readonly ChunkFactory _chunkFactory;
        private readonly IConfigProvider _configProvider;
        private readonly PipePositionGenerator _pipePositionGenerator;
        private readonly SignalBus _signalBus;
        private readonly CoinFactory _coinFactory;


        public ChunkSpawner(
            IConfigProvider configProvider,
            PipePairFactory pipePairFactory,
            ChunkFactory chunkFactory,
            CoinFactory coinFactory,
            Transform parentPath,
            PipePositionGenerator pipePositionGenerator,
            SignalBus signalBus)
        {
            _configProvider = configProvider;
            _pipePairFactory = pipePairFactory;
            _chunkFactory = chunkFactory;
            _coinFactory = coinFactory;
            _parentPath = parentPath;
            _pipePositionGenerator = pipePositionGenerator;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<ChunkInTeleportZoneSignal>(OnChunkInTeleportZone);
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
            
            _config = _configProvider.GetConfig<ChunkConfig>("ChunkConfig");
            _coinFactory.Setup(_chunksCount * _config.pipePairsCount);
            _chunkFactory.Setup(_parentPath, _chunksCount);
        }

        public LinkedList<ChunkComponent> SpawnChunks(int chunkCount, int pipePairsCount, float pipePairInterval)
        {
            var chunkWidth = pipePairsCount * pipePairInterval;
            var chunksLinkedList = new LinkedList<ChunkComponent>();
            
            for (int i = 0; i < chunkCount; i++)
            {
                var chunkLocalPosX = i * chunkWidth + _config.startPositionX;
                var chunk = _chunkFactory.Create(new Vector3(chunkLocalPosX, 0f, 0f));
                var pipePairs = new List<PipePairComponent>();
                var golds = new List<CoinComponent>();
                
                for (int k = 0; k < pipePairsCount; k++)
                {
                    var posX = k * pipePairInterval - ((pipePairsCount - 1) * pipePairInterval) / 2f;
                    var posY = _pipePositionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                    var pipePair = _pipePairFactory.Create(new Vector3(posX, posY, 0f), chunk.transform);
                    pipePairs.Add(pipePair);
                    
                    posX += pipePairInterval / 2f;
                    posY = _pipePositionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                    var gold = _coinFactory.Create(new Vector3(posX, posY, 0f), chunk.transform);
                    golds.Add(gold);
                }
                chunk.Setup(_config, pipePairs, golds);
                chunksLinkedList.AddLast(chunk);
            }
            
            return chunksLinkedList;
        }
        
        private void OnChunkInTeleportZone(ChunkInTeleportZoneSignal signal)
        {
            var chunk = signal.chunkToTeleport;
            var lastChunkPos = Chunks.Last.Value.transform.localPosition;
            var posX = lastChunkPos.x + _config.pipePairsInterval * _config.pipePairsCount;
            lastChunkPos = new Vector3(posX, lastChunkPos.y, lastChunkPos.z);
            chunk.transform.localPosition = lastChunkPos;
            Chunks.RemoveFirst();
            Chunks.AddLast(chunk);
            RespawnChunk(chunk);
            _signalBus.Fire<FirstChunkChangedSignal>(new FirstChunkChangedSignal(newFirstChunk: Chunks.First.Value));
        }
        
        private void RespawnChunk(ChunkComponent chunk)
        {
            foreach (var coin in chunk.Golds)
            {
                _coinFactory.Release(coin);
            }
            chunk.Golds.Clear();
            foreach (var pipePair in chunk.PipePairs)
            {
                var prevPos = pipePair.transform.localPosition;
                var posY = _pipePositionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                pipePair.transform.localPosition = new Vector3(prevPos.x, posY, prevPos.z);

                var posX = prevPos.x + _config.pipePairsInterval / 2f;
                posY = _pipePositionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                var gold  = _coinFactory.Create(new Vector3(posX, posY, prevPos.z), chunk.transform);
                chunk.Golds.Add(gold);
            }
        }

        private void OnGameStarted()
        {
            _chunks =  SpawnChunks(_chunksCount, _config.pipePairsCount, _config.pipePairsInterval);
            _signalBus.Fire<FirstChunkChangedSignal>(new FirstChunkChangedSignal(newFirstChunk: Chunks.First.Value));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ChunkInTeleportZoneSignal>(OnChunkInTeleportZone);
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        }
    }
}