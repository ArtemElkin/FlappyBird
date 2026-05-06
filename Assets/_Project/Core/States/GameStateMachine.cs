using System.Collections.Generic;

namespace _Project.Core.States
{
    public class GameStateMachine : BaseStateMachine<IGameState>
    {
        public GameStateMachine(List<IGameState> states) : base(states) { }
    }
}