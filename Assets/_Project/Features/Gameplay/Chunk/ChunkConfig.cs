using UnityEngine;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkConfig
    {
        public float ChunkMoveSpeed { get; private set; }
        public int PipePairsCount { get; private set; }
        public float PipePairInterval { get; private set; }
        public float PipePairOffsetY { get; private set; }
    }
}
