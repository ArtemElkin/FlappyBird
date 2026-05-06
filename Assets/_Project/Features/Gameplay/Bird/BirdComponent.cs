using _Project.Core.Data;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Coin;
using _Project.Features.Gameplay.Score;
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
        private ScoreCounter _scoreCounter;
        private SignalBus _signalBus;
        private PlayerModel _playerModel;
        
        [Inject]
        private void Construct(
            BirdMovementController movementController,
            PlayerModel playerModel,
            ScoreCounter scoreCounter,
            SignalBus signalBus)
        {
            _movementController = movementController;
            _playerModel = playerModel;
            _rb = GetComponent<Rigidbody2D>();
            _scoreCounter = scoreCounter;
            _signalBus = signalBus;
        }

        private void FixedUpdate()
        {
            Vector3 newPos = _movementController.CalculateNewLocalPosition(transform.localPosition, Time.fixedDeltaTime);
            _rb.MovePosition(newPos);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(CoinTag))
            {
                _signalBus.Fire<CoinCollectedSignal>(new CoinCollectedSignal(collision.gameObject.GetComponent<CoinComponent>()));
            }
            else if (collision.CompareTag(GapTag))
            {
                _scoreCounter.IncreaseCurrentScore();
            }
            else if (collision.CompareTag(ObstacleTag))
            {
                _signalBus.Fire<GameOverSignal>();
            }
        }
    }
}