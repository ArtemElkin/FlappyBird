using System;
using System.Collections.Generic;
using _Project.Core.States;
using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.States
{
    public class BirdStateMachine : BaseStateMachine<IBirdState>, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        public BirdStateMachine(List<IBirdState> states, SignalBus signalBus) : base(states) => _signalBus = signalBus;

        public void Initialize()
        {
            _signalBus.Subscribe<BirdCrashedSignal>(OnBirdCrashed);
        }

        private void OnBirdCrashed()
        {
            EnterState<>();
        }

        public void Dispose()
        {
        }
    }
}