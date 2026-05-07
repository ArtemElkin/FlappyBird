using UnityEngine.SceneManagement;

namespace _Project.Core.Tools
{
    public class SceneLoadService
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}