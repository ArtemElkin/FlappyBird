using _Project.Core.Signals;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Core.States
{
    public class GameplayState : IGameState
    {
        private readonly SignalBus _signalBus;
        public GameplayState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        public void Enter()
        {
            _signalBus.Fire<GameStartedSignal>();
        }

        public void Exit()
        {
        }
    }
}