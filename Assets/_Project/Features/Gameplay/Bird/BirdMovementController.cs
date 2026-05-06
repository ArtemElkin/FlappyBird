using System;
using UnityEngine;
using _Project.Core.Input;
using _Project.Features.Gameplay.States;
using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdMovementController : IJumpable, IInitializable, IDisposable
    {
        public bool IsGliding => _isGliding;
        private float _currentVelocityY;
        private bool _isGliding;
        private readonly float _jumpForce = 3.5f;
        private readonly float _glideSpeed = 1.5f;
        private readonly float _glideAmount = 1.5f;
        private readonly IInputService _inputService;
        private readonly BirdStateMachine _birdStateMachine;

        public BirdMovementController(IInputService inputService, BirdStateMachine birdStateMachine)
        {
            _inputService = inputService;
            _birdStateMachine = birdStateMachine;
        }
        
        public void Initialize()
        {
            _inputService.JumpPressed += Jump;
            _currentVelocityY = 0f;
            _isGliding = true;
        }

        public void TakeOverControl()
        {
            _isGliding = false;
            _birdStateMachine.EnterState<FlyingState>();
        }
        
        public void Jump()
        {
            if (_isGliding)
            {
                TakeOverControl();
            }
            _currentVelocityY = _jumpForce;
        }

        public Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            if (_isGliding)
            {
                var posY = currentPos.y;
                float newPosY = -Mathf.Sin(Time.time * _glideSpeed) * _glideAmount;
                return new Vector3(currentPos.x, newPosY, currentPos.z);
            }
            else
            {
                var posY = currentPos.y;
                // Y = y0 + v0t - gt^2/2
                float newY =  posY + _currentVelocityY * fixedDeltaTime - 9.8f*(fixedDeltaTime * fixedDeltaTime) / 2;
                _currentVelocityY = (newY - posY) / fixedDeltaTime;
                return new Vector3(currentPos.x, newY, currentPos.z);
            }
        }

        public void Dispose()
        {
            _inputService.JumpPressed -= Jump;
        }
    }
}
