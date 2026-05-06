using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Chunk.PipePair
{
    public class PipePairFactory
    {
        private readonly PipePairComponent _pipePairPrefab;
        private readonly IInstantiator _instantiator;
         
        
        public PipePairFactory(IInstantiator instantiator,PipePairComponent pipePairPrefab)
        {
            _instantiator = instantiator;
            _pipePairPrefab = pipePairPrefab;
        }

        public PipePairComponent Create(Vector3 localPosition, Transform parent)
        {
            var pipePair = _instantiator.InstantiatePrefabForComponent<PipePairComponent>(_pipePairPrefab, parent);
            pipePair.transform.localPosition = localPosition;
            return pipePair;
        }
    }
}