using _Project.Core.Data;
using _Project.Core.Infrastructure.Save;
using Zenject;


namespace _Project.Core.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly PlayerModel _playerModel;
        private readonly SceneLoader _sceneLoader;
        private readonly ISaveService _saveService;
        
        
        public Bootstrapper(
            SceneLoader sceneLoader,
            ISaveService saveService,
            PlayerModel playerModel)
        {
            _sceneLoader = sceneLoader;
            _saveService = saveService;
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            var playerSave = _saveService.Load<PlayerSave>();
            if (playerSave != null)
            {
                _playerModel.Load(playerSave);
            }
            
            _sceneLoader.LoadMenuScene();
        }
    }
}
