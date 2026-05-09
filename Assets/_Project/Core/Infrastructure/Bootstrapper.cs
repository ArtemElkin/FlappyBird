using _Project.Core.Data;
using Zenject;


namespace _Project.Core.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly PlayerModel _playerModel;
        private readonly SceneLoader _sceneLoader;
        
        
        public Bootstrapper(
            SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _sceneLoader.LoadMenuScene();
        }
    }
}
