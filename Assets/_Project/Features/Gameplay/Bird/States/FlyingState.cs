using UnityEngine;

namespace _Project.Features.Gameplay.Bird.States
{
    public class FlyingState : IJumpableState
    {
        private float _currentVelocityY;
        private const float _jumpForce = 3.5f;
        
        public void Enter()
        {
            _currentVelocityY = 0f;
        }

        public Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            var posY = currentPos.y;
            // Y = y0 + v0t - gt^2/2
            float newY =  posY + _currentVelocityY * fixedDeltaTime - 9.8f*(fixedDeltaTime * fixedDeltaTime) / 2;
            _currentVelocityY = (newY - posY) / fixedDeltaTime;
            return new Vector3(currentPos.x, newY, currentPos.z);
        }

        public void Jump()
        {
            _currentVelocityY = _jumpForce;
        }

        public void Exit()
        {
            
        }
    }
}