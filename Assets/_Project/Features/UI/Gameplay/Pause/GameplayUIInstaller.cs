using UnityEngine;
using Zenject;

namespace _Project.Features.UI.Gameplay.Pause
{
    public class GameplayUIInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _pauseScreen;

        public override void InstallBindings()
        {
            BindPauseController();
        }

        private void BindPauseController()
        {
            Container
                .BindInterfacesAndSelfTo<PauseController>()
                .AsSingle()
                .WithArguments(_pauseScreen);
        }
    }
}