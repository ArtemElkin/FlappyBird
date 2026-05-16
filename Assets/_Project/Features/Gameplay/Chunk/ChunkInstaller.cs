using _Project.Core.Tools;
using _Project.Features.Gameplay.Chunk.PipePair;
using _Project.Features.Gameplay.Coin;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkInstaller : MonoInstaller
    {
        [SerializeField] private Transform _chunksParentTransform;
        [SerializeField] private ChunkComponent _chunkPrefab;
        [SerializeField] private PipePairComponent _pipePairPrefab;
        
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<ChunkInWarpZoneSignal>();
            Container.DeclareSignal<FirstChunkChangedSignal>();
            
            BindPositionGenerator();
            BindPipePairFactory(_pipePairPrefab);
            BindChunkFactory(_chunkPrefab, _chunksParentTransform);
            BindChunkBuilder();
            BindChunkSpawner();
            BindChunkWarper();
            BindChunkMovementController();
        }
        
        private void BindPositionGenerator()
        {
            Container
                .Bind<PositionGenerator>()
                .AsSingle();
        }

        private void BindPipePairFactory(PipePairComponent pipePairPrefab)
        {
            Container
                .BindInterfacesAndSelfTo<PipePairFactory>()
                .AsSingle()
                .WithArguments(pipePairPrefab);
            
            Container
                .BindExecutionOrder<PipePairFactory>(-30);
        }
       
        private void BindChunkFactory(ChunkComponent chunkPrefab, Transform chunksParentTransform)
        {
            Container
                .BindInterfacesAndSelfTo<ChunkFactory>()
                .AsSingle()
                .WithArguments(chunkPrefab, chunksParentTransform);
            
            Container
                .BindInitializableExecutionOrder<ChunkFactory>(-20);
        }

        private void BindChunkBuilder()
        {
            Container
                .Bind<ChunkBuilder>()
                .AsSingle();
        }

        private void BindChunkSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<ChunkSpawner>()
                .AsSingle();
            
            Container
                .BindInitializableExecutionOrder<ChunkSpawner>(-10);
        }
        
        private void BindChunkWarper()
        {
            Container
                .BindInterfacesAndSelfTo<ChunkWarper>()
                .AsSingle();
            
            Container
                .BindInitializableExecutionOrder<ChunkWarper>(0);
        }
        
        private void BindChunkMovementController()
        {
            Container
                .Bind<ChunkMovementCalculator>()
                .AsSingle();
        }
    }
}