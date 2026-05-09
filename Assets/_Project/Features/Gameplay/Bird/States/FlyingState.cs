using UnityEngine;


namespace _Project.Features.Gameplay.Bird.States
{
    public class FlyingState : IJumpableState
    {
        private const float JumpForce = 3.5f;
        private const float MinRotationZ = -50f;
        private const float MaxRotationZ = 50f;
        private const float MinVelocityY = -5f;
        private const float MaxVelocityY = 5f;
        private float _currentVelocityY;
        private Quaternion _minRotation;
        private Quaternion _maxRotation;
        
        public void Enter()
        {
            _currentVelocityY = 0f;
            _minRotation = Quaternion.Euler(0, 0, MinRotationZ);
            _maxRotation = Quaternion.Euler(0, 0, MaxRotationZ);
        }

        public Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            var posY = currentPos.y;
            // Y = y0 + v0t - gt^2/2
            float newY =  posY + _currentVelocityY * fixedDeltaTime - 9.8f*(fixedDeltaTime * fixedDeltaTime) / 2;
            _currentVelocityY = (newY - posY) / fixedDeltaTime;
            return new Vector3(currentPos.x, newY, currentPos.z);
        }

        public Quaternion CalculateNewLocalRotation(Quaternion currentRotation, float fixedDeltaTime)
        {
            var t = Mathf.InverseLerp(MinVelocityY, MaxVelocityY, _currentVelocityY);
            var newRotation = Quaternion.Lerp(_minRotation, _maxRotation, t);
            newRotation = Quaternion.Lerp(currentRotation, newRotation, 0.5f);
            return newRotation;
        }

        public void Jump()
        {
            _currentVelocityY = JumpForce;
        }

        public void Exit()
        {
            
        }
    }
}