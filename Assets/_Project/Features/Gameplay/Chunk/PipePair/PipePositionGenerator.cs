using System;

namespace _Project.Features.Gameplay.Chunk.PipePair
{
    public class PipePositionGenerator
    {
        private int _scale = 100;
        private readonly Random _random;

        public PipePositionGenerator()
        {
            _random = new Random();
        }
        public float GenerateRandomPositionY(float maxOffsetY)
        {
            var minYScaled = (int)(-maxOffsetY * _scale);
            var maxYScaled = (int)(maxOffsetY * _scale);
            int newPosYScaled = _random.Next(minYScaled, maxYScaled);
            float newPosY = newPosYScaled / 1f / _scale;
            return newPosY;
        }
    }
}