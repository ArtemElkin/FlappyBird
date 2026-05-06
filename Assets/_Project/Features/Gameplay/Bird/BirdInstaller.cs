using _Project.Features.Gameplay.Signals;
using _Project.Features.Gameplay.States;
using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.DeclareSignal<BirdActivatedSignal>();
            Container.DeclareSignal<BirdCrashedSignal>();
            
            Container.Bind<IBirdState>().To<IdleState>().AsSingle();
            Container.Bind<IBirdState>().To<FlyingState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BirdStateMachine >().AsSingle();

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