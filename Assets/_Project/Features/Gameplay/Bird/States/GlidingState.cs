using UnityEngine;

namespace _Project.Features.Gameplay.Bird.States
{
    public class GlidingState : IState
    {
        private const float GlideSpeed = 1.5f;
        private const float GlideAmount = 1.5f;
        public void Enter()
        {
        }

        public Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            var posY = currentPos.y;
            float newPosY = -Mathf.Sin(Time.time * GlideSpeed) * GlideAmount;
            return new Vector3(currentPos.x, newPosY, currentPos.z);
        }

        public void Exit()
        {
        }
    }
}