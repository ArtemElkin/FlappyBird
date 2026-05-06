using UnityEngine.SceneManagement;

namespace _Project.Core.States
{
    public class GameplayState : IGameState
    {
        private const string GameplaySceneName = "Game";
        public void Enter()
        {
            SceneManager.LoadScene(GameplaySceneName);
        }

        public void Exit()
        {
        }
    }
}