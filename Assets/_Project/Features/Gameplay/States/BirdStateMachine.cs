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
        private readonly GameStateMachine _gameStateMachine;

        public BirdStateMachine(List<IBirdState> states, SignalBus signalBus, GameStateMachine gameStateMachine) :
            base(states)
        {
            _signalBus = signalBus;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BirdCrashedSignal>(OnBirdCrashed);
        }

        private void OnBirdCrashed()
        {
            _gameStateMachine.EnterState<GameOverState>();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BirdCrashedSignal>(OnBirdCrashed);
        }
    }
}