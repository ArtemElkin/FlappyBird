using _Project.Core.Ads;
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
        private readonly IAdsService _adsService;
        
        
        public Bootstrapper(
            SceneLoader sceneLoader,
            ISaveService saveService,
            PlayerModel playerModel,
            IAdsService  adsService)
        {
            _sceneLoader = sceneLoader;
            _saveService = saveService;
            _playerModel = playerModel;
            _adsService = adsService;
        }

        public void Initialize()
        {
            var playerSave = _saveService.Load<PlayerSave>();
            if (playerSave != null)
            {
                _playerModel.Load(playerSave);
            }
            
            _adsService.Initialize();
            
            _sceneLoader.LoadMenuScene();
        }
    }
}
