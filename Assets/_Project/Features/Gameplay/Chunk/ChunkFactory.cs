using _Project.Core.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkFactory
    {
        private CustomPool<ChunkComponent> _chunkPool;
        private readonly ChunkComponent _chunkPrefab;
        private readonly IInstantiator _instantiator;
        
        public ChunkFactory(IInstantiator instantiator, ChunkComponent chunkPrefab)
        {
            _instantiator = instantiator;
            _chunkPrefab = chunkPrefab;
        }

        public void Setup(Transform parentTransform, int preWarmChunksCount)
        {
            _chunkPool = new CustomPool<ChunkComponent>(_instantiator, _chunkPrefab, preWarmChunksCount, parentTransform);
        }

        public ChunkComponent Create(Vector3 localPosition)
        {
            var chunk = _chunkPool.Get();
            chunk.transform.localPosition = localPosition;
            return chunk;
        }
        
    }
}