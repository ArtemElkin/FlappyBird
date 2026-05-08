using System;

namespace _Project.Core.Tools
{
    public class PositionGenerator
    {
        private const int Scale = 100;
        private readonly Random _random;

        public PositionGenerator(int? seed = null)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }
        public float GenerateRandomPositionY(float maxOffsetY)
        {
            var minYScaled = (int)(-maxOffsetY * Scale);
            var maxYScaled = (int)(maxOffsetY * Scale);
            int newPosYScaled = _random.Next(minYScaled, maxYScaled);
            float newPosY = newPosYScaled / 1f / Scale;
            return newPosY;
        }
    }
}