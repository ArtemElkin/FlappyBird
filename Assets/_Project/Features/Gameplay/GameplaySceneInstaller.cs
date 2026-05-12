using _Project.Core.Tools;
using Zenject;

namespace _Project.Features.Gameplay
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScreenBoundsCalculator();
        }
        
        private void BindScreenBoundsCalculator()
        {
            Container
                .Bind<ScreenBoundsCalculator>()
                .AsSingle();
        }
    }
}