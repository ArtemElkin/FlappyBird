using _Project.Core.Tools;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Chunk.PipePair
{
    public class PipePairFactory : IInitializable
    {
        private CustomPool<PipePairComponent> _pipePairsPool;
        private readonly PipePairComponent _pipePairPrefab;
        private readonly IInstantiator _instantiator;
        
        
        public PipePairFactory(IInstantiator instantiator,PipePairComponent pipePairPrefab)
        {
            _instantiator = instantiator;
            _pipePairPrefab = pipePairPrefab;
        }

        public void Initialize()
        {
            _pipePairsPool = new CustomPool<PipePairComponent>(_instantiator, _pipePairPrefab);
        }

        public PipePairComponent Create(Vector3 localPosition, Transform parent)
        {
            
            var pipePair = _pipePairsPool.Get(parent);
            pipePair.transform.localPosition = localPosition;
            return pipePair;
        }

        public void Release(PipePairComponent pipePair)
        {
            _pipePairsPool.Release(pipePair);
        }
    }
}