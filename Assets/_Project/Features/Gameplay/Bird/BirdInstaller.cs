using _Project.Features.Gameplay.Signals;
using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.DeclareSignal<BirdActivatedSignal>();

            BindBirdMovementController();
        }

        private void BindBirdMovementController()
        {
            Container
                .BindInterfacesAndSelfTo<BirdMovementController>()
                .AsSingle();
        }
    }
}