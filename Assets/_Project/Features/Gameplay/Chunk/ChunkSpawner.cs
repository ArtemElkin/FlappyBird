using System;
using System.Collections.Generic;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Pipe;
using UnityEngine;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkSpawner
    {
        private ChunkConfig _config;
        private CustomPool<ChunkComponent> _chunkPool;
        private PipePairSpawner _pipePairSpawner;
        private ChunkComponent _chunkPrefab;
        private Transform _parentPath;


        public ChunkSpawner(ChunkConfig config, ChunkComponent chunkPrefab, Transform parentPath)
        {
            _config = config;
            _chunkPrefab = chunkPrefab;
            _parentPath = parentPath;
            _pipePairSpawner = new PipePairSpawner();
        }

        public LinkedList<ChunkComponent> SpawnChunks(int count)
        {
            _chunkPool = new CustomPool<ChunkComponent>(_chunkPrefab, count, _parentPath);
            var linkedList = new LinkedList<ChunkComponent>();
            for (int i = 0; i < count; i++)
            {
                var chunk =  _chunkPool.Get();

                var last = linkedList.Last.Value;
                var lastPosX = last == null ? 0f : last.transform.localPosition.x;
                
                var newLocalPosX = lastPosX + _config.PipePairsCount * _config.PipePairInterval;
                
                chunk.Initialize(_config,newLocalPosX);
                
                linkedList.AddLast(_chunkPool.Get());
            }
            
            return linkedList;
        }
    }
}