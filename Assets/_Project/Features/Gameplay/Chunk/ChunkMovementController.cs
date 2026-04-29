using UnityEngine;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkMovementController
    {
        private float _speed;

        public ChunkMovementController(float speed)
        {
            _speed = speed;
        }

        public Vector3 CalculateNewPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            currentPos.x -= _speed * fixedDeltaTime;
            return currentPos;
        }
    }
}