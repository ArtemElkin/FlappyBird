using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Coin
{
    public class CoinInstaller : MonoInstaller
    {
        [SerializeField] private CoinComponent _coinPrefab;
        [SerializeField] private Transform _parentTransform;
        
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<CoinCollectedSignal>();
            
            BindCoinFactory();
            BindCoinsCollector();
        }

        private void BindCoinFactory()
        {
            Container
                .BindInterfacesAndSelfTo<CoinFactory>()
                .AsSingle()
                .WithArguments(_coinPrefab, _parentTransform);
            
            Container
                .BindExecutionOrder<CoinFactory>(-30);
        }

        private void BindCoinsCollector()
        {
            Container
                .BindInterfacesAndSelfTo<CoinCollector>()
                .AsSingle();
        }
    }
}