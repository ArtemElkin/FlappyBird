using System.Collections.Generic;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Background
{
    public class BackgroundInstaller : MonoInstaller
    {
        [SerializeField] private BackgroundLayerComponent _backgroundLayerPrefab;
        [SerializeField] private List<BackgroundLayer> _backgrounds;
        [SerializeField] private Transform _backgroundParentTransform;

        
        public override void InstallBindings()
        {
            Container.DeclareSignal<BackgroundInWarpZoneSignal>();
            Container.DeclareSignal<FirstBackgroundChangedSignal>();
            
            BindBackgroundmMovementCalculator();
            BindBackgroundSpawner();
            BindBackgroundWarper();
        }
        
        private void BindBackgroundmMovementCalculator()
        {
            Container
                .Bind<BackgroundMovementCalculator>()
                .AsSingle();
        }
        private void BindBackgroundSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<BackgroundSpawner>()
                .AsSingle()
                .WithArguments(_backgroundLayerPrefab, _backgroundParentTransform);
        }

        private void BindBackgroundWarper()
        {
            Container
                .BindInterfacesAndSelfTo<BackgroundWarper>()
                .AsSingle();
        }
    }
}