using _Project.Core.Signals;
using _Project.Features.Gameplay.Coin;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdComponent : MonoBehaviour
    {
        private const string CoinTag = "Coin";
        private const string GapTag = "Gap";
        private const string ObstacleTag = "Obstacle";
        private BirdMovementController _movementController;
        private Rigidbody2D _rb;
        private SignalBus _signalBus;
        
        
        [Inject]
        private void Construct(
            BirdMovementController movementController,
            SignalBus signalBus)
        {
            _movementController = movementController;
            _signalBus = signalBus;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var newPosition = _movementController.CalculateNewLocalPosition(transform.localPosition, Time.fixedDeltaTime);
            _rb.MovePosition(newPosition);
            
            var newRotation = _movementController.CalculateNewLocalRotation(transform.localRotation, Time.fixedDeltaTime);
            _rb.MoveRotation(newRotation);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(CoinTag))
            {
                _signalBus.Fire<CoinCollectedSignal>(new CoinCollectedSignal(collision.gameObject.GetComponent<CoinComponent>()));
            }
            else if (collision.CompareTag(GapTag))
            {
                _signalBus.Fire<GapPassedSignal>();
            }
            else if (collision.CompareTag(ObstacleTag))
            {
                _signalBus.Fire<GameOverSignal>();
            }
        }
    }
}