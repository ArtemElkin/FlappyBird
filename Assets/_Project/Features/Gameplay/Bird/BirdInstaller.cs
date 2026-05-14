using _Project.Features.Gameplay.Bird.States;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdInstaller : MonoInstaller
    {
        [SerializeField] private BirdComponent _birdPrefab;
        
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<BirdActivatedSignal>();
            Container.DeclareSignal<GapPassedSignal>();
            
            Container.Bind<BirdStateMachine>().AsSingle();
            
            BindBirdMovementController();
            BindBirdSpawner(_birdPrefab);
        }

        private void BindBirdMovementController()
        {
            Container
                .BindInterfacesAndSelfTo<BirdMovementController>()
                .AsSingle();
        }

        private void BindBirdSpawner(BirdComponent birdPrefab)
        {
            Container
                .BindInterfacesAndSelfTo<BirdSpawner>()
                .AsSingle()
                .WithArguments(birdPrefab);
        }
    }
}