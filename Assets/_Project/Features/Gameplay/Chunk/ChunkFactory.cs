using _Project.Core.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkFactory
    {
        private ChunkComponent _chunkPrefab;
        private IInstantiator _instantiator;
        private CustomPool<ChunkComponent> _chunkPool;
        
        public ChunkFactory(IInstantiator instantiator, ChunkComponent chunkPrefab)
        {
            _instantiator = instantiator;
            _chunkPrefab = chunkPrefab;
        }

        public void Setup(Transform parentPath, int preWarmChunksCount)
        {
            _chunkPool = new CustomPool<ChunkComponent>(_instantiator, _chunkPrefab, preWarmChunksCount, parentPath);
        }

        public ChunkComponent Create(Vector3 localPosition)
        {
            var chunk = _chunkPool.Get();
            chunk.transform.localPosition = localPosition;
            return chunk;
        }
        
    }
}