using System.Collections.Generic;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Chunk.PipePair;
using _Project.Features.Gameplay.Coin;
using UnityEngine;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkBuilder
    {
        private float _moveSpeed;
        private int _elementsPerChunk;
        private float _intervalBetweenElements;
        private Vector3 _initialPosition;
        private List<PipePairComponent> _pipePairs;
        private readonly List<Vector3> _pipePairsPositions = new();
        private readonly List<Vector3> _coinsPositions = new();
        private readonly PositionGenerator _positionGenerator;
        private readonly PipePairFactory _pipePairFactory;
        private readonly CoinFactory _coinFactory;
        private readonly ChunkFactory _chunkFactory;


        public ChunkBuilder(
            PositionGenerator positionGenerator, 
            PipePairFactory pipePairFactory,
            CoinFactory coinFactory,
            ChunkFactory chunkFactory)
        {
            _positionGenerator = positionGenerator;
            _pipePairFactory = pipePairFactory;
            _coinFactory = coinFactory;
            _chunkFactory = chunkFactory;
        }

        public ChunkBuilder SetSpeed(float speed)
        {
            _moveSpeed = speed;
            return this;
        }

        public ChunkBuilder SetInitialPosition(Vector3 position)
        {
            _initialPosition = position;
            return this;
        }

        public ChunkBuilder SetElementsPerChunk(int elementsPerChunk)
        {
            _elementsPerChunk = elementsPerChunk;
            return this;
        }

        public ChunkBuilder SetIntervalBetweenElements(float intervalBetweenElements)
        {
            _intervalBetweenElements = intervalBetweenElements;
            return this;
        }

        public ChunkBuilder AddPipePairs(float maxOffsetY)
        {
            _pipePairsPositions.Clear();
            
            for (int i = 0; i < _elementsPerChunk; i++)
            {
                var posX = i * _intervalBetweenElements - ((_elementsPerChunk - 1) * _intervalBetweenElements) / 2f;
                var posY = _positionGenerator.GenerateRandomPositionY(maxOffsetY);
                _pipePairsPositions.Add(new Vector3(posX, posY, 0f));
            }
            return this;
        }

        public ChunkBuilder AddCoins(float maxOffsetY)
        {
            _coinsPositions.Clear();
            
            for (int i = 0; i < _elementsPerChunk; i++)
            {
                var posX = i * _intervalBetweenElements - ((_elementsPerChunk - 1) * _intervalBetweenElements) / 2f;
                posX += _intervalBetweenElements / 2f;
                var posY = _positionGenerator.GenerateRandomPositionY(maxOffsetY);
                _coinsPositions.Add(new Vector3(posX, posY, 0f));
            }
            return this;
        }

        public ChunkComponent Build()
        {
            var chunk = _chunkFactory.Create(_initialPosition);
            var pipePairs = new List<PipePairComponent>();
            var coins = new List<CoinComponent>();
            foreach (var pos in _pipePairsPositions)
            {
                var pipePair = _pipePairFactory.Create(pos, chunk.transform);
                pipePairs.Add(pipePair);
            }
            foreach (var pos in _coinsPositions)
            {
                var coin = _coinFactory.Create(pos, chunk.transform);
                coins.Add(coin);
            }
            chunk.Setup(_moveSpeed, pipePairs, coins);
            return chunk;
        }
        
    }
}