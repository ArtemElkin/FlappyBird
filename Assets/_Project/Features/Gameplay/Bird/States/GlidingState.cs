using UnityEngine;

namespace _Project.Features.Gameplay.Bird.States
{
    public class GlidingState : IState
    {
        private const float GlideSpeed = 1.5f;
        private const float GlideAmount = 1.5f;
        private float _currentVelocityY;
        private const float MinRotationZ = -50f;
        private const float MaxRotationZ = 50f;
        private const float MinVelocityY = -5f;
        private const float MaxVelocityY = 5f;
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
            float newPosY = -Mathf.Sin(Time.time * GlideSpeed) * GlideAmount;
            _currentVelocityY = (newPosY - posY) / fixedDeltaTime;
            return new Vector3(currentPos.x, newPosY, currentPos.z);
        }
        
        public Quaternion CalculateNewLocalRotation(Quaternion currentRotation, float fixedDeltaTime)
        {
            var t = Mathf.InverseLerp(MinVelocityY, MaxVelocityY, _currentVelocityY);
            var newRotation = Quaternion.Lerp(_minRotation, _maxRotation, t);
            return newRotation;
        }

        public void Exit()
        {
        }
    }
}