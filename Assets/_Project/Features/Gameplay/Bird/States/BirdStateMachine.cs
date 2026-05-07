using System;
using System.Collections.Generic;

namespace _Project.Features.Gameplay.Bird.States
{
    public class BirdStateMachine
    {
        public IState ActiveState { get; private set; }
        private readonly Dictionary<Type, IState> _states = new();
        
        
        public BirdStateMachine(List<IState> states)
        {
            foreach (var state in states)
            {
                _states[state.GetType()] = state;
            }
        }
        
        public void EnterState<T>() where T : class, IState
        {
            if (!_states.ContainsKey(typeof(T)))
            {
                return;
            }
            
            ActiveState?.Exit();
            ActiveState = _states[typeof(T)];
            ActiveState.Enter();
        }
    }
}