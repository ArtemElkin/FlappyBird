using System;
using UnityEngine;
using _Project.Core.Input;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Bird.States;
using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdMovementController : IJumpable, IInitializable, IDisposable
    {
        private bool _isMoving;
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
            _isMoving = true;
            _inputService.JumpPressed += Jump;
            _birdStateMachine.EnterState<GlidingState>();
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

        public Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            return _isMoving ? _birdStateMachine.ActiveState.CalculateNewLocalPosition(currentPos, fixedDeltaTime) : currentPos;
        }

        public Quaternion CalculateNewLocalRotation(Quaternion currentRotation, float fixedDeltaTime)
        {
            return _isMoving? _birdStateMachine.ActiveState.CalculateNewLocalRotation(currentRotation, fixedDeltaTime) :  currentRotation;
        }

        private void OnGameOver()
        {
            _isMoving = false;
        }

        private void OnGameRestarted()
        {
            _isMoving = true;
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
