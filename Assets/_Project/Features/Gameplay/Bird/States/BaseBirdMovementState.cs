using UnityEngine;


namespace _Project.Features.Gameplay.Bird.States
{
    public abstract class BaseBirdMovementState : IState
    {
        private readonly float _minRotationZ;
        private readonly float _maxRotationZ;
        private readonly float _minVelocityY;
        private readonly float _maxVelocityY;
        protected float _currentVelocityY;
        private Quaternion _minRotation;
        private Quaternion _maxRotation;


        protected BaseBirdMovementState(float minRotationZ, float maxRotationZ, float minVelocityY, float maxVelocityY)
        {
            _minRotationZ = minRotationZ;
            _maxRotationZ = maxRotationZ;
            _minVelocityY = minVelocityY;
            _maxVelocityY = maxVelocityY;
        }

        public void Enter()
        {
            _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
            _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);

            _currentVelocityY = 0f;
        }

        public abstract Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime);

        public virtual Quaternion CalculateNewLocalRotation(Quaternion currentRotation, float fixedDeltaTime)
        {
            var t = Mathf.InverseLerp(_minVelocityY, _maxVelocityY, _currentVelocityY);
            var newRotation = Quaternion.Lerp(_minRotation, _maxRotation, t);
            newRotation = Quaternion.Lerp(currentRotation, newRotation, 0.6f);
            return newRotation;
        }

        public void Exit()
        {
            
        }
    }
}