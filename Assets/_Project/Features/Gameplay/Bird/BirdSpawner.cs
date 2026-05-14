using System;
using _Project.Core.Signals;
using Zenject;
using UnityEngine;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdSpawner : IInitializable
    {
        private readonly BirdComponent _birdPrefab;
        private readonly IInstantiator _instantiator;
        
        
        public BirdSpawner(
            BirdComponent birdPrefab,
            IInstantiator instantiator)
        {
            _birdPrefab = birdPrefab;
            _instantiator = instantiator;
        }
        
        public void Initialize()
        {
            SpawnBird();
        }

        private void SpawnBird()
        {
            _instantiator.InstantiatePrefabForComponent<BirdComponent>(_birdPrefab);
        }
    }
}