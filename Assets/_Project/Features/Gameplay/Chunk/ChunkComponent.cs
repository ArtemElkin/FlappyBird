using _Project.Core.Tools;
using _Project.Features.Gameplay.Pipe;
using UnityEngine;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkComponent : MonoBehaviour
    {
        [SerializeField] private PipePairComponent[] _pipePairs;
        private ChunkMovementController _movementController;
        private PipePositionGenerator _pipePositionGenerator;
        private CustomPool<PipePairComponent> _pipePairsPool;
        private ChunkConfig _config;
        

        public void Initialize(ChunkConfig config, float initLocalPosX)
        {
            _config = config;
            var chunkMovementController = new ChunkMovementController(_config.ChunkMoveSpeed);
            var pipePositionGenerator = new PipePositionGenerator(_config.PipePairOffsetY);
            transform.localPosition = new Vector3(initLocalPosX, transform.localPosition.y, transform.localPosition.z);
        }

        public void RespawnPipes()
        {
            foreach (var pipePair in _pipePairs)
            {
                var newPosY = _pipePositionGenerator.GenerateRandomPositionY();
                pipePair.SetNewLocalPositionY(newPosY);
                pipePair.gameObject.SetActive(true);
            }
        }

        private void FixedUpdate()
        {
            var newPos = _movementController.CalculateNewPosition(transform.localPosition, Time.fixedDeltaTime);
            transform.localPosition = newPos;
        }
    }
}
