using UnityEngine.SceneManagement;

namespace _Project.Core.States
{
    public class MenuState : IGameState
    {
        private const string MenuSceneName = "MainMenu";
        
        public void Enter()
        {
            SceneManager.LoadScene(MenuSceneName);
        }

        public void Exit()
        {
        }
    }
}