using Zenject;

namespace _Project.Features.Gameplay.Score
{
    public class ScoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScoreCounter();
        }
        private void BindScoreCounter()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreCounter>()
                .AsSingle();
        }
    }
}