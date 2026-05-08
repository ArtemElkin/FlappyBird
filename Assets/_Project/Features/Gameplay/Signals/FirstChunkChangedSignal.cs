using _Project.Features.Gameplay.Chunk;
using UnityEngine;

namespace _Project.Features.Gameplay.Signals
{
    public class FirstChunkChangedSignal
    {
        public ChunkComponent newFirstChunk;

        public FirstChunkChangedSignal(ChunkComponent newFirstChunk)
        {
            this.newFirstChunk = newFirstChunk;
        }
    }
}