using _Project.Features.Gameplay.Chunk;

namespace _Project.Features.Gameplay.Signals
{
    public class ChunkInWarpZoneSignal
    {
        public readonly ChunkComponent chunkToWarp;
        
        public ChunkInWarpZoneSignal(ChunkComponent chunkToWarp) => this.chunkToWarp = chunkToWarp;
    }
}