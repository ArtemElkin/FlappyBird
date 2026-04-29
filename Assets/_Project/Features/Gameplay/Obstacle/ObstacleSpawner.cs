using _Project.Core.Tools;
using UnityEngine;

namespace _Project.Features.Gameplay.Obstacle
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _obstaclePrefab;
        private CustomPool<ObstacleComponent> _obstaclePool;


        private void Initialize()
        {
            //_obstaclePool = new CustomPool<ObstacleComponent>(_obstaclePrefab, 10,);
        }
        
        private void SpawnObstacles()
        {
            
        }
    }
}
