using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Pipe;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkSpawner : IInitializable
    {
        public LinkedList<ChunkComponent> Chunks => _chunks;
        private LinkedList<ChunkComponent> _chunks;
        private ChunkConfig _config;
        private CustomPool<ChunkComponent> _chunkPool;
        private Transform _parentPath;
        
        private PipePairFactory _pipePairFactory;
        private ChunkFactory _chunkFactory;
        private IInstantiator _instantiator;
        private IConfigProvider _configProvider;
        private PipePositionGenerator _pipePositionGenerator;
        private ChunkMovementCalculator _chunkMovementCalculator;

        
        public ChunkSpawner(
            IConfigProvider configProvider,
            PipePairFactory pipePairFactory,
            ChunkFactory chunkFactory,
            Transform parentPath,
            PipePositionGenerator pipePositionGenerator,
            ChunkMovementCalculator chunkMovementCalculator)
        {
            _configProvider = configProvider;
            _pipePairFactory = pipePairFactory;
            _chunkFactory = chunkFactory;
            _parentPath = parentPath;
            _pipePositionGenerator = pipePositionGenerator;
            _chunkMovementCalculator = chunkMovementCalculator;
        }
        
        public void Initialize()
        {
            _config = _configProvider.GetConfig<ChunkConfig>("ChunkConfig");
            var chunksCount = 4;
            _chunkFactory.Setup(_parentPath, chunksCount);
            _chunks =  SpawnChunks(chunksCount, _config.pipePairsCount, _config.pipePairsInterval);
        }

        public LinkedList<ChunkComponent> SpawnChunks(int chunkCount, int pipePairsCount, float pipePairInterval)
        {
            float chunkWidth = pipePairsCount * pipePairInterval;
            float chunkOffsetX = (chunkWidth * chunkCount) / 2f;
            float pipePairOffsetX = chunkWidth / 2f;
            float lastPipePairWorldPosX = 0f;
            
            var chunksLinkedList = new LinkedList<ChunkComponent>();
            
            for (int i = 0; i < chunkCount; i++)
            {
                var chunkLocalPosX = i * chunkWidth + _config.startPositionX;
                var chunk = _chunkFactory.Create(new Vector3(chunkLocalPosX, 0f, 0f));
                var pipePairs = new PipePairComponent[_config.pipePairsCount];
                
                for (int k = 0; k < pipePairsCount; k++)
                {
                    var posX = k * pipePairInterval - ((pipePairsCount - 1) * pipePairInterval) / 2f;
                    var posY = _pipePositionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                    var pipePair = _pipePairFactory.Create(new Vector3(posX, posY, 0f), chunk.transform);
                    pipePairs[k] = pipePair;
                }
                chunk.Setup(_config, pipePairs, _pipePositionGenerator, _chunkMovementCalculator);
                chunksLinkedList.AddLast(chunk);
            }
            
            return chunksLinkedList;
        }
    }
}