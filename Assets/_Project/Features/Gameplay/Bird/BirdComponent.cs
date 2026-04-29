using _Project.Core.Input;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdComponent : MonoBehaviour
    {
        private BirdMovementController _movementController;
        
        [Inject]
        private void Construct(BirdMovementController movementController)
        {
            _movementController = movementController;
        }

        private void FixedUpdate()
        {
            Vector3 newPos = _movementController.CalculateNewLocalPosition(transform.localPosition, Time.fixedDeltaTime);
            transform.localPosition = newPos;
        }

        private void OnDestroy()
        {
            _movementController.Dispose();
        }
    }
}