using System;

namespace _Project.Features.Gameplay.Pipe
{
    public class PipePositionGenerator
    {
        private float _offset;
        private int _minYScaled;
        private int _maxYScaled;
        private int _scale = 100;

        public PipePositionGenerator(float offset)
        {
            _offset = offset;
            _minYScaled = (int)(-_offset * _scale);
            _maxYScaled = (int)(_offset * _scale);
            
        }
        public float GenerateRandomPositionY()
        {
            Random random = new Random();
            int newPosYScaled = random.Next(_minYScaled, _maxYScaled);
            float newPosY = newPosYScaled / 1f / _scale;
            return newPosY;
        }
    }
}