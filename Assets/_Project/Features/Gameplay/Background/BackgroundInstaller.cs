using System.Collections.Generic;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Background
{
    public class BackgroundInstaller : MonoInstaller
    {
        [SerializeField] private BackgroundLayerComponent _backgroundLayerPrefab;
        [SerializeField] private Transform _backgroundParentTransform;

        
        public override void InstallBindings()
        {
            Container.DeclareSignal<BackgroundInWarpZoneSignal>();
            Container.DeclareSignal<FirstBackgroundChangedSignal>();
            
            BindBackgroundmMovementCalculator();
            BindBackgroundFactory(_backgroundLayerPrefab, _backgroundParentTransform);
            BindBackgroundSpawner();
            BindBackgroundWarper();
        }
        
        private void BindBackgroundmMovementCalculator()
        {
            Container
                .Bind<BackgroundMovementCalculator>()
                .AsSingle();
        }

        private void BindBackgroundFactory(BackgroundLayerComponent backgroundLayerPrefab, Transform backgroundParentTransform)
        {
            Container
                .BindInterfacesAndSelfTo<BackgroundFactory>()
                .AsSingle()
                .WithArguments(backgroundLayerPrefab, backgroundParentTransform);
        }
        private void BindBackgroundSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<BackgroundSpawner>()
                .AsSingle();
        }

        private void BindBackgroundWarper()
        {
            Container
                .BindInterfacesAndSelfTo<BackgroundWarper>()
                .AsSingle();
        }
    }
}