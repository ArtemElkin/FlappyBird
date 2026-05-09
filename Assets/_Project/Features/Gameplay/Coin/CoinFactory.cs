using _Project.Core.Tools;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Coin
{
    public class CoinFactory
    {
        private CustomPool<CoinComponent> _goldPool;
        private readonly CoinComponent _coinPrefab;
        private readonly IInstantiator _instantiator;
        private readonly Transform _defaultParentTransform;

        
        public CoinFactory(IInstantiator instantiator, CoinComponent coinPrefab, Transform defaultParentTransform)
        {
            _instantiator = instantiator;
            _coinPrefab = coinPrefab;
            _defaultParentTransform = defaultParentTransform;
        }
        
        public void Setup(int preWarmGoldCount)
        {
            _goldPool = new CustomPool<CoinComponent>(_instantiator, _coinPrefab, preWarmGoldCount, _defaultParentTransform);
        }
        public CoinComponent Create(Vector3 localPosition, Transform parentTransform)
        {
            var gold = _goldPool.Get(parentTransform);
            gold.transform.localPosition = localPosition;
            return gold;
        }

        public void Release(CoinComponent coin)
        {
            _goldPool.Release(coin);
        }
    }
}