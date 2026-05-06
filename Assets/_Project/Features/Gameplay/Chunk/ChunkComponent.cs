using System;
using System.Collections.Generic;
using _Project.Features.Gameplay.Bird;
using _Project.Features.Gameplay.Chunk.Pipe;
using _Project.Features.Gameplay.Coin;
using _Project.Features.Gameplay.Signals;
using _Project.Features.Gameplay.States;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkComponent : MonoBehaviour, IDisposable
    {
        public List<CoinComponent> Golds => _golds;
        public List<PipePairComponent> PipePairs => _pipePairs;
        private List<CoinComponent> _golds;
        private List<PipePairComponent> _pipePairs;
        private ChunkMovementCalculator _movementCalculator;
        private PipePositionGenerator _pipePositionGenerator;
        private ChunkConfig _config;
        private SignalBus _signalBus;
        private bool _movementIsActive;


        [Inject]
        public void Construct(
            PipePositionGenerator pipePositionGenerator,
            ChunkMovementCalculator movementCalculator,
            SignalBus signalBus)
        {
            _pipePositionGenerator = pipePositionGenerator;
            _movementCalculator = movementCalculator;
            _signalBus = signalBus;
            _signalBus.Subscribe<BirdActivatedSignal>(ActivateMovement);
            _signalBus.Subscribe<BirdCrashedSignal>(DeactivateMovement);;
        }

        public void Setup(ChunkConfig config, List<PipePairComponent> pipePairs, List<CoinComponent> golds)
        {
            _config =  config;
            _pipePairs = pipePairs;
            _golds = golds;
        }

        private void ActivateMovement()
        {
            _movementIsActive = true;
        }

        private void DeactivateMovement()
        {
            _movementIsActive = false;
        }

        private void FixedUpdate()
        {
            if (!_movementIsActive) return;
            var newPos = _movementCalculator.CalculateNewPosition(transform.localPosition, _config.moveSpeed,Time.fixedDeltaTime);
            transform.localPosition = newPos;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BirdActivatedSignal>(ActivateMovement);
            _signalBus.Unsubscribe<BirdCrashedSignal>(DeactivateMovement);
        }
    }
}
