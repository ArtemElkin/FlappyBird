using System;
using _Project.Core.Signals;
using Zenject;
using UnityEngine;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdSpawner : IInitializable, IDisposable
    {
        private BirdComponent _bird;
        private BirdMovementController _birdMovementController;
        private readonly BirdComponent _birdPrefab;
        private readonly SignalBus _signalBus;
        private readonly IInstantiator _instantiator;
        
        public BirdSpawner(
            BirdComponent birdPrefab,
            BirdMovementController birdMovementController,
            SignalBus signalBus,
            IInstantiator instantiator)
        {
            _birdPrefab = birdPrefab;
            _birdMovementController = birdMovementController;
            _signalBus = signalBus;
            _instantiator = instantiator;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        public void RespawnBird()
        {
            if (_birdPrefab == null)
            {
                _bird = _instantiator.InstantiatePrefabForComponent<BirdComponent>(_birdPrefab);
            }
            _bird.transform.localPosition = Vector3.zero;
            _birdMovementController.Reset();
        }

        private void OnGameStarted()
        {
            RespawnBird();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        }
    }
}