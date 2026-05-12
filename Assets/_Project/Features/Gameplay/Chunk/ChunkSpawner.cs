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
        private readonly int _chunksCount = 4;
        private readonly IConfigProvider _configProvider;
        private readonly PositionGenerator _positionGenerator;
        private readonly ChunkFactory _chunkFactory;
        private readonly PipePairFactory _pipePairFactory;
        private readonly CoinFactory _coinFactory;
        private readonly SignalBus _signalBus;
        private readonly ChunkWarper _chunkWarper;
        private readonly ScreenBoundsCalculator _screenBoundsCalculator;


        public ChunkSpawner(
            IConfigProvider configProvider,
            PipePairFactory pipePairFactory,
            ChunkFactory chunkFactory,
            CoinFactory coinFactory,
            PositionGenerator positionGenerator,
            ChunkWarper chunkWarper,
            SignalBus signalBus,
            ScreenBoundsCalculator screenBoundsCalculator)
        {
            _configProvider = configProvider;
            _pipePairFactory = pipePairFactory;
            _chunkFactory = chunkFactory;
            _coinFactory = coinFactory;
            _positionGenerator = positionGenerator;
            _signalBus = signalBus;
            _chunkWarper = chunkWarper;
            _screenBoundsCalculator = screenBoundsCalculator;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<ChunkInWarpZoneSignal>(OnChunkInWarpZone);
            _signalBus.Subscribe<GameRestartedSignal>(OnGameRestarted);
            
            _config = _configProvider.GetConfigFromJson<ChunkConfig>("ChunkConfig");
            _coinFactory.Setup(_chunksCount * _config.pipePairsCount);
            _chunkFactory.Setup(_chunksCount);
            
            _chunks =  SpawnChunks(_chunksCount, _config.pipePairsCount, _config.pipePairsInterval);
            _chunkWarper.Setup(Chunks.First.Value);
        }

        public LinkedList<ChunkComponent> SpawnChunks(int chunkCount, int pipePairsCount, float pipePairsInterval)
        {
            var chunkWidth = pipePairsCount * pipePairsInterval;
            var chunksLinkedList = new LinkedList<ChunkComponent>();
            
            for (int i = 0; i < chunkCount; i++)
            {
                var chunkLocalPosX = i * chunkWidth + _screenBoundsCalculator.RightEdgeX + chunkWidth / 2;
                var chunk = _chunkFactory.Create(new Vector3(chunkLocalPosX, 0f, 0f));
                var pipePairs = new List<PipePairComponent>();
                var golds = new List<CoinComponent>();
                
                for (int k = 0; k < pipePairsCount; k++)
                {
                    var posX = k * pipePairsInterval - ((pipePairsCount - 1) * pipePairsInterval) / 2f;
                    var posY = _positionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                    var pipePair = _pipePairFactory.Create(new Vector3(posX, posY, 0f), chunk.transform);
                    pipePairs.Add(pipePair);
                    
                    posX += pipePairsInterval / 2f;
                    posY = _positionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                    var gold = _coinFactory.Create(new Vector3(posX, posY, 0f), chunk.transform);
                    golds.Add(gold);
                }
                chunk.Setup(_config, pipePairs, golds);
                chunksLinkedList.AddLast(chunk);
            }
            
            return chunksLinkedList;
        }
        
        private void OnChunkInWarpZone(ChunkInWarpZoneSignal signal)
        {
            var chunk = signal.chunkToWarp;
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
            foreach (var coin in chunk.Coins)
            {
                _coinFactory.Release(coin);
            }
            chunk.Coins.Clear();
            foreach (var pipePair in chunk.PipePairs)
            {
                var prevPos = pipePair.transform.localPosition;
                var posY = _positionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                pipePair.transform.localPosition = new Vector3(prevPos.x, posY, prevPos.z);

                var posX = prevPos.x + _config.pipePairsInterval / 2f;
                posY = _positionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                var gold  = _coinFactory.Create(new Vector3(posX, posY, prevPos.z), chunk.transform);
                chunk.Coins.Add(gold);
            }
        }

        private void OnGameRestarted()
        {
            var chunkWidth = _config.pipePairsCount * _config.pipePairsInterval;
            List<ChunkComponent> chunks = new List<ChunkComponent>();
            foreach (var ch in Chunks)
            {
                chunks.Add(ch);
            }
            Chunks.Clear();
            for (int i = 0; i < _chunksCount; i++)
            {
                var chunkLocalPosX = i * chunkWidth + _screenBoundsCalculator.RightEdgeX + chunkWidth / 2;
                chunks[i].transform.localPosition = new Vector3(chunkLocalPosX, 0f, 0f);
                RespawnChunk(chunks[i]);
                Chunks.AddLast(chunks[i]);
            }
            _signalBus.Fire<FirstChunkChangedSignal>(new FirstChunkChangedSignal(newFirstChunk: Chunks.First.Value));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ChunkInWarpZoneSignal>(OnChunkInWarpZone);
            _signalBus.Unsubscribe<GameRestartedSignal>(OnGameRestarted);
        }
    }
}