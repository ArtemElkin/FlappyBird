using System;
using UnityEngine;
using System.Collections;
using _Project.Core.Input;
using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdMovementController : IJumpable, IInitializable, IDisposable
    {
        private IInputService _inputService;
        private float _currentVelocityY;
        private float _jumpForce = 3.5f;

        [Inject]
        public BirdMovementController(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public void Initialize()
        {
            _inputService.JumpPressed += Jump;
            _currentVelocityY = 0f;
        }
        
        public void Jump()
        {
            _currentVelocityY = _jumpForce;
        }

        public Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            var posY = currentPos.y;
            // Y = y0 + v0t - gt^2/2
            float newY =  posY + _currentVelocityY * fixedDeltaTime - 9.8f*(fixedDeltaTime * fixedDeltaTime) / 2;
            _currentVelocityY = (newY - posY) / fixedDeltaTime;
            var newPos = new Vector3(currentPos.x, newY, currentPos.z);
            return newPos;
        }

        public void Dispose()
        {
            _inputService.JumpPressed -= Jump;
        }
    }
}
