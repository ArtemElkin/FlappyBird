using System;
using _Project.Core.Signals;
using Zenject;
using UnityEngine;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdSpawner : IInitializable, IDisposable
    {
        private BirdComponent _bird;
        private readonly BirdComponent _birdPrefab;
        private readonly SignalBus _signalBus;
        private readonly IInstantiator _instantiator;
        
        
        public BirdSpawner(
            BirdComponent birdPrefab,
            SignalBus signalBus,
            IInstantiator instantiator)
        {
            _birdPrefab = birdPrefab;
            _signalBus = signalBus;
            _instantiator = instantiator;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<GameRestartedSignal>(OnGameRestarted);
            RespawnBird();
        }

        private void RespawnBird()
        {
            if (_bird == null)
            {
                _bird = _instantiator.InstantiatePrefabForComponent<BirdComponent>(_birdPrefab);
            }
            _bird.transform.localPosition = Vector3.zero;
        }

        private void OnGameRestarted()
        {
            RespawnBird();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameRestartedSignal>(OnGameRestarted);
        }
    }
}