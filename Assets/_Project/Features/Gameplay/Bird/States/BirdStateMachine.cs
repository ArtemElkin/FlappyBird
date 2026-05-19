using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using Zenject;


namespace _Project.Features.Gameplay.Bird.States
{
    public class BirdStateMachine : IInitializable
    {
        public IState ActiveState { get; private set; }
        private readonly Dictionary<Type, IState> _states = new();
        private readonly IConfigProvider _configProvider;        
        
        public BirdStateMachine(IConfigProvider  configProvider)
        {
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfigFromJson<BirdMovementConfig>("BirdMovementConfig");
            _states[typeof(GlidingState)] = new GlidingState(
                config.minRotationZ, 
                config.maxRotationZ, 
                config.minVelocityY, 
                config.maxVelocityY,
                config.glideSpeed,
                config.glideAmount);
            _states[typeof(FlyingState)] = new FlyingState(
                config.minRotationZ,
                config.maxRotationZ,
                config.minVelocityY,
                config.maxVelocityY,
                config.jumpForce);
        }
        
        public void EnterState<T>() where T : class, IState
        {
            if (!_states.ContainsKey(typeof(T)))
            {
                throw new Exception($"State of type {typeof(T)} does not exist");
            }
            
            ActiveState?.Exit();
            ActiveState = _states[typeof(T)];
            ActiveState.Enter();
        }
    }
}