using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Core.States
{
    public abstract class BaseStateMachine<TStateInterface> where TStateInterface : IState
    {
        private readonly Dictionary<Type, TStateInterface> _states = new();
        private IState _activeState;
        
        
        protected BaseStateMachine(List<TStateInterface> states)
        {
            foreach (var state in states)
            {
                _states[state.GetType()] = state;
            }
        }
        
        public void EnterState<T>() where T : class, TStateInterface
        {
            if (!_states.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Стейт {typeof(T).Name} не зарегистрирован в машине!");
                return;
            }
            
            _activeState?.Exit();
            _activeState = _states[typeof(T)];
            _activeState.Enter();
        }
    }
}