using System;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Chunk;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Coin
{
    public class CoinFactory : IInitializable
    {
        private CustomPool<CoinComponent> _coinsPool;
        private readonly CoinComponent _coinPrefab;
        private readonly IInstantiator _instantiator;
        private readonly Transform _defaultParentTransform;

        
        public CoinFactory(IInstantiator instantiator, CoinComponent coinPrefab, Transform defaultParentTransform)
        {
            _instantiator = instantiator;
            _coinPrefab = coinPrefab;
            _defaultParentTransform = defaultParentTransform;
        }

        public void Initialize()
        {
            _coinsPool = new CustomPool<CoinComponent>(_instantiator, _coinPrefab, defaultParentTransform: _defaultParentTransform);
        }
        
        public CoinComponent Create(Vector3 localPosition, Transform parentTransform)
        {
            var coin = _coinsPool.Get(parentTransform);
            coin.transform.localPosition = localPosition;
            return coin;
        }

        public void Release(CoinComponent coin)
        {
            _coinsPool.Release(coin);
        }
    }
}