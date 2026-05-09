using _Project.Core.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace _Project.Features.UI.MainMenu
{
    public class PlayButton : MonoBehaviour
    {
        private Button _button;
        private SignalBus _signalBus;
        
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _button = GetComponent<Button>();
        }

        private void OnButtonClick()
        {
            _signalBus.Fire<StartGameClickedSignal>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
