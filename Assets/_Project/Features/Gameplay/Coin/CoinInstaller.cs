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
            
            BindGoldFactory();
            BindGoldCollector();
        }

        private void BindGoldFactory()
        {
            Container
                .Bind<CoinFactory>()
                .AsSingle()
                .WithArguments(_coinPrefab, _parentTransform);
        }

        private void BindGoldCollector()
        {
            Container
                .BindInterfacesAndSelfTo<CoinCollector>()
                .AsSingle();
        }
    }
}