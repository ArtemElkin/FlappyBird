using _Project.Core.Tools;
using _Project.Features.Gameplay.Ads;
using Zenject;

namespace _Project.Features.Gameplay
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScreenBoundsCalculator();
            BindGameplayAdsController();
        }
        
        private void BindScreenBoundsCalculator()
        {
            Container
                .Bind<ScreenBoundsCalculator>()
                .AsSingle();
        }
        
        private void BindGameplayAdsController()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayAdsController>()
                .AsSingle();
        }
    }
}