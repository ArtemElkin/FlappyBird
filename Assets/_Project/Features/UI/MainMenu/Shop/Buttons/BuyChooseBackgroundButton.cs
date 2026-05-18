using _Project.Features.UI.Signals;
using TMPro;
using UnityEngine;


namespace _Project.Features.UI.MainMenu.Shop.Buttons
{
    public class BuyChooseBackgroundButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _text;

        
        protected override void OnClick()
        {
            _signalBus.Fire<BuyChooseBackgroundButtonClickedSignal>();
        }

        public void UpdateText(string text)
        {
            _text.text = text;
        }
    }
}