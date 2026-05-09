using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace _Project.Features.UI
{
    public abstract class BaseButton :  MonoBehaviour
    {
        private Button _button;
        protected SignalBus _signalBus;
        

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _button = GetComponent<Button>();
        }

        protected abstract void OnClick();
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}