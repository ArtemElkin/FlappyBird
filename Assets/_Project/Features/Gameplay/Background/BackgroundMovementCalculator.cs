using UnityEngine;

namespace _Project.Features.Gameplay.Background
{
    public class BackgroundMovementCalculator
    {
        public Vector3 CalculateNewPosition(Vector3 currentPos, float speed, float fixedDeltaTime)
        {
            currentPos.x -= speed * fixedDeltaTime;
            return currentPos;
        }
    }
}