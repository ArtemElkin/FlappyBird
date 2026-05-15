using _Project.Core.Tools;
using _Project.Features.Gameplay.Ads;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        
        
        public override void InstallBindings()
        {
            BindScreenBoundsCalculator(_camera);
            BindGameplayAdsController();
        }
        
        private void BindScreenBoundsCalculator(Camera mainCamera)
        {
            Container
                .Bind<ScreenBoundsCalculator>()
                .AsSingle()
                .WithArguments(mainCamera);
        }
        
        private void BindGameplayAdsController()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayAdsController>()
                .AsSingle();
        }
    }
}