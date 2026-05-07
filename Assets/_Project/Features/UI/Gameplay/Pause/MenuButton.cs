using _Project.Core.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace _Project.Features.UI.Gameplay.Pause
{
    public class MenuButton : MonoBehaviour
    {
        private Button _button;
        private SignalBus _signalBus;

        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        

        private void OnButtonClick()
        {
            _signalBus.Fire<MenuClickedSignal>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}