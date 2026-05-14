using _Project.Core.Signals;
using _Project.Features.Gameplay.Chunk;
using _Project.Features.Gameplay.Chunk.PipePair;
using _Project.Features.Gameplay.Coin;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdComponent : MonoBehaviour
    {
        private SignalBus _signalBus;
        
        
        [Inject]
        private void Construct(
            BirdMovementController movementController,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            var rb = GetComponent<Rigidbody2D>();
            
            movementController.Setup(transform, rb);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<GapComponent>(out _))
            {
                _signalBus.Fire<GapPassedSignal>();
            }
            else if (collision.gameObject.TryGetComponent<CoinComponent>(out var coin))
            {
                _signalBus.Fire<CoinCollectedSignal>(new CoinCollectedSignal(coin));
            }
            else if (collision.gameObject.TryGetComponent<ObstacleComponent>(out _))
            {
                _signalBus.Fire<GameOverSignal>();
            }
        }
    }
}