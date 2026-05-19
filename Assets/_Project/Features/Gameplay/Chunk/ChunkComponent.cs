using System;
using System.Collections.Generic;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Chunk.PipePair;
using _Project.Features.Gameplay.Coin;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkComponent : MonoBehaviour
    {
        private bool _isSetup;
        private float _moveSpeed;
        public List<CoinComponent> Coins => _coins;
        public List<PipePairComponent> PipePairs => _pipePairs;
        private List<CoinComponent> _coins;
        private List<PipePairComponent> _pipePairs;
        private ChunkMovementCalculator _movementCalculator;
        private SignalBus _signalBus;
        private bool _movementIsActive;
        private Rigidbody2D _rb;


        private void OnEnable()
        {
            _signalBus.Subscribe<BirdActivatedSignal>(ActivateMovement);
            _signalBus.Subscribe<GameOverSignal>(DeactivateMovement);
        }
        
        private void FixedUpdate()
        {
            if (!_isSetup) return;
            if (!_movementIsActive) return;
            var newPos = _movementCalculator.CalculateNewPosition(transform.localPosition, _moveSpeed,Time.fixedDeltaTime);
            _rb.MovePosition(newPos);
        }
        
        [Inject]
        private void Construct(
            ChunkMovementCalculator movementCalculator,
            SignalBus signalBus)
        {
            _movementCalculator = movementCalculator;
            _signalBus = signalBus;
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Setup(float moveSpeed, List<PipePairComponent> pipePairs, List<CoinComponent> coins)
        {
            _moveSpeed = moveSpeed;
            _pipePairs = pipePairs;
            _coins = coins;
            _isSetup = true;
        }

        private void ActivateMovement() => _movementIsActive = true;

        private void DeactivateMovement() => _movementIsActive = false;
        
        private void OnDisable()
        {
            _signalBus.Unsubscribe<BirdActivatedSignal>(ActivateMovement);
            _signalBus.Unsubscribe<GameOverSignal>(DeactivateMovement);
        }
    }
}
