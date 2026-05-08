using UnityEngine;

namespace _Project.Features.Gameplay.Bird.States
{
    public interface IState
    {
        void Enter();
        Vector3 CalculateNewLocalPosition(Vector3 currentPos, float fixedDeltaTime);
        Quaternion CalculateNewLocalRotation(Quaternion currentRot, float fixedDeltaTime);
        void Exit();
    }
}