using _Project.Features.Gameplay.Chunk;

namespace _Project.Features.Gameplay.Signals
{
    public class ChunkInTeleportZoneSignal
    {
        public readonly ChunkComponent chunkToTeleport;
        
        public ChunkInTeleportZoneSignal(ChunkComponent chunkToTeleport) => this.chunkToTeleport = chunkToTeleport;
    }
}