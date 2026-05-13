using _Project.Core.Ads;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Infrastructure.Save;
using _Project.Core.Player;
using Zenject;


namespace _Project.Core.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly PlayerModel _playerModel;
        private readonly SceneLoader _sceneLoader;
        private readonly ISaveService _saveService;
        private readonly IAdsService _adsService;
        private readonly IConfigProvider _configProvider;
        
        
        public Bootstrapper(
            SceneLoader sceneLoader,
            ISaveService saveService,
            PlayerModel playerModel,
            IAdsService  adsService,
            IConfigProvider configProvider)
        {
            _sceneLoader = sceneLoader;
            _saveService = saveService;
            _playerModel = playerModel;
            _adsService = adsService;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var playerSave = _saveService.Load<PlayerSave>();
            if (playerSave != null)
            {
                _playerModel.Load(playerSave);
            }

            var adsConfig = _configProvider.GetConfigFromJson<AdUnitsIdsConfig>("AdUnitsIdsConfig");
            _adsService.Initialize(adsConfig);
            
            _sceneLoader.LoadMenuScene();
        }
    }
}
