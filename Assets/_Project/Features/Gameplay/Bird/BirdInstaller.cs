using _Project.Core.Data;
using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBirdMovementController();
            Container.DeclareSignal<BirdCrashedSignal>();

            Container.BindSignal<BirdCrashedSignal>()
                .ToMethod<PlayerModel>(model => model.SetAlive);
        }

        private void BindBirdMovementController()
        {
            Container
                .BindInterfacesAndSelfTo<BirdMovementController>()
                .AsSingle();
        }
    }
}