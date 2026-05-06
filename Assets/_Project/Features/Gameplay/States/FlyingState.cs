using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.States
{
    public class FlyingState : IBirdState
    {
        private readonly SignalBus _signalBus;
        
        public FlyingState(SignalBus signalBus) => _signalBus = signalBus;
        
        public void Enter() => _signalBus.Fire<BirdActivatedSignal>();

        public void Exit()
        {
        }
    }
}