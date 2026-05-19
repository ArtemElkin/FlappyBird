using _Project.Core.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay
{
    public class GameplayStarter : MonoBehaviour
    {
        private SignalBus _signalBus;


        private void Start()
        {
            _signalBus.Fire<GameStartedSignal>();
        }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
    }
}