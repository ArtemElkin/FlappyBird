using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Background
{
    public class BackgroundComponent : MonoBehaviour
    {
        public Background background;
        private SpriteRenderer _spriteRenderer;
        private BackgroundMovementCalculator _movementCalculator;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        [Inject]
        private void Construct(BackgroundMovementCalculator movementCalculator)
        {
            _movementCalculator = movementCalculator;
        }
        
        public void Setup(Background backgroundConfig)
        {
            background = backgroundConfig;
            _spriteRenderer.sprite = background.sprite;
            _spriteRenderer.sortingOrder = background.order;
        }
        
        private void FixedUpdate()
        {
            var newPos = _movementCalculator.CalculateNewPosition(transform.localPosition, background.speed,Time.fixedDeltaTime);
            transform.localPosition = newPos;
        }
    }
}