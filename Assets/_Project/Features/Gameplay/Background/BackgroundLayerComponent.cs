using _Project.Core.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Background
{
    public class BackgroundLayerComponent : MonoBehaviour
    {
        public BackgroundLayer backgroundLayer;
        private bool _movementIsActive;
        private SpriteRenderer _spriteRenderer;
        private BackgroundMovementCalculator _movementCalculator;
        private SignalBus _signalBus;

        
        private void OnEnable()
        {
            _signalBus.Subscribe<GameRestartedSignal>(ActivateMovement);
            _signalBus.Subscribe<GameOverSignal>(DeactivateMovement);
        }

        private void FixedUpdate()
        {
            if (!_movementIsActive)  return;
            var newPos = _movementCalculator.CalculateNewPosition(transform.localPosition, backgroundLayer.speed,Time.fixedDeltaTime);
            transform.localPosition = newPos;
        }

        [Inject]
        private void Construct(BackgroundMovementCalculator movementCalculator, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _movementCalculator = movementCalculator;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Setup(BackgroundLayer backgroundLayerConfig)
        {
            backgroundLayer = backgroundLayerConfig;
            _spriteRenderer.sprite = backgroundLayer.sprite;
            _spriteRenderer.sortingOrder = backgroundLayer.order;
            _movementIsActive = true;
        }

        private void ActivateMovement() => _movementIsActive = true;

        private void DeactivateMovement() => _movementIsActive = false;

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameRestartedSignal>(ActivateMovement);
            _signalBus.Unsubscribe<GameOverSignal>(DeactivateMovement);
        }
    }
}