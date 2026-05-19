using UnityEngine;


namespace _Project.Features.Gameplay.Bird.States
{
    public class GlidingState : BaseBirdMovementState
    {
        private readonly float _glideSpeed;
        private readonly float _glideAmount;

        
        public GlidingState(float minRotationZ, float maxRotationZ, float minVelocityY, float maxVelocityY, float glideSpeed, float glideAmount) 
            : base(minRotationZ, maxRotationZ, minVelocityY, maxVelocityY)
        {
            _glideSpeed = glideSpeed;
            _glideAmount = glideAmount;
        }

        public override Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            var posY = currentPos.y;
            float newPosY = -Mathf.Sin(Time.time * _glideSpeed) * _glideAmount;
            _currentVelocityY = (newPosY - posY) / fixedDeltaTime;
            return new Vector3(currentPos.x, newPosY, currentPos.z);
        }
    }
}