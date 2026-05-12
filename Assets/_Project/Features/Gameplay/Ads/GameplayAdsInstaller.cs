using Zenject;

namespace _Project.Features.Gameplay.Ads
{
    public class GameplayAdsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameplayAdsController();
        }

        private void BindGameplayAdsController()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayAdsController>()
                .AsSingle();
        }
        
    }
}