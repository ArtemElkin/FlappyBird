using _Project.Core.Input;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdComponent : MonoBehaviour
    {
        private BirdMovementController _movementController;
        private Rigidbody2D _rb;
        
        [Inject]
        private void Construct(BirdMovementController movementController)
        {
            _movementController = movementController;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector3 newPos = _movementController.CalculateNewLocalPosition(transform.localPosition, Time.fixedDeltaTime);
            _rb.MovePosition(newPos);
        }

        private void OnDestroy()
        {
            _movementController.Dispose();
        }
    }
}