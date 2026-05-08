using _Project.Core.Infrastructure.Config;
using UnityEngine;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkConfig : IConfig
    {
        public float moveSpeed;
        public int pipePairsCount;
        public float pipePairsInterval;
        public float pipePairOffsetY;
        public float startPositionX;
        public float warpPositionX;
    }
}
