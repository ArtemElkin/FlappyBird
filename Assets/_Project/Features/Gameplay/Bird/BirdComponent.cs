using _Project.Core.Data;
using _Project.Features.Gameplay.Gold;
using _Project.Features.Gameplay.Score;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdComponent : MonoBehaviour
    {
        private BirdMovementController _movementController;
        private Rigidbody2D _rb;
        private ScoreCounter _scoreCounter;
        private GoldCollector _goldCollector;
        private SignalBus _signalBus;
        private PlayerModel _playerModel;
        
        [Inject]
        private void Construct(
            BirdMovementController movementController,
            PlayerModel playerModel,
            ScoreCounter scoreCounter,
            GoldCollector goldCollector,
            SignalBus signalBus)
        {
            _movementController = movementController;
            _playerModel = playerModel;
            _rb = GetComponent<Rigidbody2D>();
            _scoreCounter = scoreCounter;
            _goldCollector = goldCollector;
            _signalBus = signalBus;
        }

        private void FixedUpdate()
        {
            Vector3 newPos = _movementController.CalculateNewLocalPosition(transform.localPosition, Time.fixedDeltaTime);
            _rb.MovePosition(newPos);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Gold"))
            {
                _goldCollector.CollectGold(1);
            }
            else if (collision.CompareTag("Gap"))
            {
                _scoreCounter.IncreaseCurrentScore();
            }
            else if (collision.CompareTag("Obstacle"))
            {
                _signalBus.Fire(new BirdCrashedSignal());
            }
        }
    }
}