using Zenject;

namespace _Project.Features.Gameplay.Gold
{
    public class GoldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGoldCollector();
        }

        private void BindGoldCollector()
        {
            Container
                .Bind<GoldCollector>()
                .AsSingle();
        }
    }
}