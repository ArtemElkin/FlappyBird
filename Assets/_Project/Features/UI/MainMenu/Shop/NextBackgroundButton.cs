using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Features.UI.MainMenu.Shop
{
    public class NextBackgroundButton : MonoBehaviour
    {
        private Button _button;
        private SignalBus _signalBus;


        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnClick()
        {
            _signalBus.Fire<>
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}