using _Project.Features.Gameplay.Pipe;
using UnityEngine;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkComponent : MonoBehaviour
    {
        public PipePairComponent[] PipePairs => _pipePairs;
        private PipePairComponent[] _pipePairs;
        private ChunkMovementCalculator _movementCalculator;
        private PipePositionGenerator _pipePositionGenerator;
        private ChunkConfig _config;
        

        public void Setup(
            ChunkConfig config, 
            PipePairComponent[] pipePairs, 
            PipePositionGenerator pipePositionGenerator,
            ChunkMovementCalculator movementCalculator)
        {
            _config = config;
            _pipePairs = pipePairs;
            _pipePositionGenerator = pipePositionGenerator;
            _movementCalculator = movementCalculator;
        }

        public void RespawnPipesPositions()
        {
            foreach (var pipePair in _pipePairs)
            {
                var newPosY = _pipePositionGenerator.GenerateRandomPositionY(_config.pipePairOffsetY);
                pipePair.SetNewLocalPositionY(newPosY);
                pipePair.gameObject.SetActive(true);
            }
        }

        private void FixedUpdate()
        {
            var newPos = _movementCalculator.CalculateNewPosition(transform.localPosition, _config.moveSpeed,Time.fixedDeltaTime);
            transform.localPosition = newPos;
        }
    }
}
