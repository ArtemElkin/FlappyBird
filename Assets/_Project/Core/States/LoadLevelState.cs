using UnityEngine.SceneManagement;

namespace _Project.Core.States
{
    public class LoadLevelState : IGameState
    {
        private const string GameplaySceneName = "Game";
        
        public LoadLevelState()
        {
            // _stateMachine = stateMachine;    
        }
        public void Enter()
        {
            SceneManager.LoadScene(GameplaySceneName);
            // _stateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}