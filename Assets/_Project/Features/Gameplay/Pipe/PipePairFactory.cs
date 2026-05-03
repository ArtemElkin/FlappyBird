using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Pipe
{
    public class PipePairFactory
    {
        private PipePairComponent _pipePairPrefab;
        private IInstantiator _instantiator;
         
        
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