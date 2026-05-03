using _Project.Features.Gameplay.Pipe;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkInstaller : MonoInstaller
    {
        [SerializeField] private Transform _chunksParent;
        [SerializeField] private ChunkComponent _chunkPrefab;
        [SerializeField] private PipePairComponent _pipePairPrefab;
        
        
        public override void InstallBindings()
        {
            
            BindPipePairFactory(_pipePairPrefab);
            BindChunkFactory(_chunkPrefab);
            BindChunkSpawner(_chunksParent);
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
        
        private void BindChunkFactory(ChunkComponent chunkPrefab)
        {
            Container
                .Bind<ChunkFactory>()
                .AsSingle()
                .WithArguments(chunkPrefab);
        }

        private void BindChunkSpawner(Transform chunksParent)
        {
            Container
                .BindInterfacesAndSelfTo<ChunkSpawner>()
                .AsSingle()
                .WithArguments(chunksParent);
            
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