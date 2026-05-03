using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkTeleporter : IInitializable, ITickable
    {

        private LinkedList<ChunkComponent> _chunksLinkedList;
        private Transform _firstChunkTransform;
        private ChunkSpawner _chunkSpawner;
        private ChunkConfig _chunkConfig;
        private IConfigProvider _configProvider;


        public ChunkTeleporter(ChunkSpawner chunkSpawner, IConfigProvider configProvider)
        {
            _chunkSpawner = chunkSpawner;
            _configProvider = configProvider;
        }
        
        public void Initialize()
        {
            _chunkConfig = _configProvider.GetConfig<ChunkConfig>("ChunkConfig");
            _chunksLinkedList = _chunkSpawner.Chunks;
            _firstChunkTransform = _chunksLinkedList.First.Value.transform;
        }
        
        public void Tick()
        {
            CheckFirstChunk();
        }
        
        private void CheckFirstChunk()
        {
            var pos =  _firstChunkTransform.localPosition;
            if (pos.x < _chunkConfig.teleportationPositionX)
            {
                var tmp = _chunksLinkedList.First.Value;
                var posX = _chunksLinkedList.Last.Value.transform.localPosition.x + _chunkConfig.pipePairsInterval * _chunkConfig.pipePairsCount;
                pos = new Vector3(posX, pos.y, pos.z);
                _firstChunkTransform.localPosition = pos;
                _chunksLinkedList.RemoveFirst();
                _chunksLinkedList.AddLast(tmp);
                tmp.RespawnPipesPositions();
                _firstChunkTransform =  _chunksLinkedList.First.Value.transform;
            }
        }
    }
}
