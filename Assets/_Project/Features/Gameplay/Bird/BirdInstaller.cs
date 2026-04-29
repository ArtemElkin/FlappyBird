using Zenject;

namespace _Project.Features.Gameplay.Bird
{
    public class BirdInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBirdMovementController();
        }

        private void BindBirdMovementController()
        {
            Container.Bind<BirdMovementController>().AsTransient();
        }
    }
}