using _Project.Features.Gameplay.Chunk.PipePair;
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
            Container.DeclareSignal<ChunkInTeleportZoneSignal>();
            Container.DeclareSignal<FirstChunkChangedSignal>();
            
            BindPipePairFactory(_pipePairPrefab);
            BindChunkFactory(_chunkPrefab, _chunksParentTransform);
            BindChunkSpawner();
            BindChunkTeleporter();
            BindChunkMovementController();
            BindPipePositionGenerator();
        }

        private void BindPipePairFactory(PipePairComponent pipePairPrefab)
        {
            Container
                .Bind<PipePairFactory>()
                .AsSingle()
                .WithArguments(pipePairPrefab);
        }
        
        private void BindChunkFactory(ChunkComponent chunkPrefab, Transform chunksParentTransform)
        {
            Container
                .Bind<ChunkFactory>()
                .AsSingle()
                .WithArguments(chunkPrefab, chunksParentTransform);
        }

        private void BindChunkSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<ChunkSpawner>()
                .AsSingle();
            
            Container
                .BindInitializableExecutionOrder<ChunkSpawner>(-10);
        }
        
        private void BindChunkTeleporter()
        {
            Container
                .BindInterfacesAndSelfTo<ChunkTeleporter>()
                .AsSingle();
            
            Container
                .BindInitializableExecutionOrder<ChunkTeleporter>(0);
        }
        
        private void BindChunkMovementController()
        {
            Container
                .Bind<ChunkMovementCalculator>()
                .AsSingle();
        }

        private void BindPipePositionGenerator()
        {
            Container
                .Bind<PipePositionGenerator>()
                .AsSingle();
        }
    }
}