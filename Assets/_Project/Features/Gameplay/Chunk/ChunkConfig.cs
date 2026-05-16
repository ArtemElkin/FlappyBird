using _Project.Core.Infrastructure.Config;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkConfig : IConfig
    {
        public int chunksCount;
        public float moveSpeed;
        public int elementsPerChunk;
        public float intervalBetweenElements;
        public float maxElementOffsetY;
    }
}
