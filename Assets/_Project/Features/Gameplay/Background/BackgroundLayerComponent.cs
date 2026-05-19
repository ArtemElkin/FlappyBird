using System.Collections.Generic;
using _Project.Core.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Background
{
    public class BackgroundLayerComponent : MonoBehaviour
    {
        public BackgroundLayer Layer { get; private set; }
        private bool _isSetup;
        private int _groupdId;
        private bool _movementIsActive;
        private SpriteRenderer _spriteRenderer;
        private BackgroundMovementCalculator _movementCalculator;
        private SignalBus _signalBus;
        public int GroupId => _groupdId;

        
        private void OnEnable()
        {
            _signalBus.Subscribe<GameStartedSignal>(ActivateMovement);
            _signalBus.Subscribe<GameRestartedSignal>(ActivateMovement);
            _signalBus.Subscribe<GameOverSignal>(DeactivateMovement);
        }

        private void FixedUpdate()
        {
            if (!_isSetup) return;
            if (!_movementIsActive)  return;
            var newPos = _movementCalculator.CalculateNewPosition(transform.localPosition, Layer.speed,Time.fixedDeltaTime);
            transform.localPosition = newPos;
        }

        [Inject]
        private void Construct(BackgroundMovementCalculator movementCalculator, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _movementCalculator = movementCalculator;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Setup(BackgroundLayer backgroundLayer, int groupId)
        {
            Layer = backgroundLayer;
            _groupdId = groupId;
            _spriteRenderer.sprite = Layer.sprite;
            _spriteRenderer.sortingOrder = Layer.order;
            _isSetup = true;
        }

        private void ActivateMovement() => _movementIsActive = true;

        private void DeactivateMovement() => _movementIsActive = false;

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(ActivateMovement);
            _signalBus.Unsubscribe<GameRestartedSignal>(ActivateMovement);
            _signalBus.Unsubscribe<GameOverSignal>(DeactivateMovement);
        }
    }
}