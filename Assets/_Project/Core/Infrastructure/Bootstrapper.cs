using _Project.Core.Data;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Infrastructure.Save;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Core.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private IConfigProvider _configProvider;
        private ISaveService _saveService;
        private PlayerModel _playerModel;
        
        [Inject]
        private void Construct(
            IConfigProvider configProvider,
            ISaveService saveService,
            PlayerModel playerModel)
        {
            _configProvider = configProvider;
            _saveService = saveService;
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            var save = _saveService.Load<PlayerProgress>() ?? new PlayerProgress();
            
            _playerModel.Setup(save);

            SceneManager.LoadScene("MainMenu");
        }
    }
}
