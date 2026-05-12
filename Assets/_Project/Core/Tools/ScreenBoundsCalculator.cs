using UnityEngine;


namespace _Project.Core.Tools
{
    public class ScreenBoundsCalculator
    {
        private readonly Camera _camera = Camera.main;

        public float RightEdgeX => _camera.transform.position.x + (_camera.orthographicSize * _camera.aspect);
        
        public float LeftEdgeX => _camera.transform.position.x - (_camera.orthographicSize * _camera.aspect);
        
        public float TopEdgeY => _camera.transform.position.y + _camera.orthographicSize;
        
        public float BottomEdgeY => _camera.transform.position.y - _camera.orthographicSize;
    }
}