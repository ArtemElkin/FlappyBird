using System;
using UnityEngine;
using _Project.Core.Input;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Bird.States;
using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdMovementController : IJumpable, IInitializable, IDisposable, IFixedTickable
    {
        private bool _isMoving;
        private Transform _birdTransform;
        private Rigidbody2D _rb;
        private readonly IInputService _inputService;
        private readonly SignalBus _signalBus;
        private readonly BirdStateMachine  _birdStateMachine;
        

        public BirdMovementController(
            IInputService inputService,
            SignalBus signalBus,
            BirdStateMachine birdStateMachine)
        {
            _inputService = inputService;
            _signalBus = signalBus;
            _birdStateMachine = birdStateMachine;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<GameRestartedSignal>(OnGameRestarted);
            _inputService.JumpPressed += Jump;
        }

        public void Setup(Transform birdTransform, Rigidbody2D rb)
        {
            _birdTransform = birdTransform;
            _rb = rb;
            
            Reset();
        }

        public void FixedTick()
        {
            if (!_isMoving) return;
            
            var newPosition = CalculateNewLocalPosition(Time.fixedDeltaTime);
            _rb.MovePosition(newPosition);
            
            var newRotation = CalculateNewLocalRotation(Time.fixedDeltaTime);
            _rb.MoveRotation(newRotation);
        }
        
        public void Jump()
        {
            if (_birdStateMachine.ActiveState is GlidingState)
            {
                _birdStateMachine.EnterState<FlyingState>();
                _signalBus.Fire<BirdActivatedSignal>();
            }
            if (_birdStateMachine.ActiveState is IJumpableState jumpableState)
            {
                jumpableState.Jump();
            }
        }

        private Vector3 CalculateNewLocalPosition(float fixedDeltaTime)
        {
            return _birdStateMachine.ActiveState.CalculateNewLocalPosition(_birdTransform.localPosition, fixedDeltaTime);
        }

        private Quaternion CalculateNewLocalRotation(float fixedDeltaTime)
        {
            return _birdStateMachine.ActiveState.CalculateNewLocalRotation(_birdTransform.localRotation, fixedDeltaTime);
        }

        private void Reset()
        {
            _birdTransform.localPosition = Vector3.zero;
            _birdTransform.localRotation = Quaternion.identity;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _isMoving = true;
            _birdStateMachine.EnterState<GlidingState>();
        }

        private void OnGameOver()
        {
            _isMoving = false;
        }

        private void OnGameRestarted()
        {
            _isMoving = true;
            Reset();
            _birdStateMachine.EnterState<GlidingState>();
        }

        public void Dispose()
        {
            _inputService.JumpPressed -= Jump;
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _signalBus.Unsubscribe<GameRestartedSignal>(OnGameRestarted);
        }
    }
}
