using _Project.Core.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Features.UI.Gameplay.Pause
{
    public class RestartButton : MonoBehaviour
    {
        private Button _button;
        private SignalBus _signalBus;

        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnButtonClick()
        {
            _signalBus.Fire<GameRestartedSignal>();
            Debug.Log("Restart clicked. Signal sent");
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
        
    }
}