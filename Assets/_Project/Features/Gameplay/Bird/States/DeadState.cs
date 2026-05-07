using UnityEngine;

namespace _Project.Features.Gameplay.Bird.States
{
    public class DeadState : IState
    {
        public void Enter()
        {
            
        }

        public Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime)
        {
            return currentPos;
        }

        public void Exit()
        {
            
        }
    }
}