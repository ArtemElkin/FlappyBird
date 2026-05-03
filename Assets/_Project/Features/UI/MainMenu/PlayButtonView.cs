using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Features.UI.MainMenu
{
    public class PlayButtonView : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            SceneManager.LoadScene("Game");
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
