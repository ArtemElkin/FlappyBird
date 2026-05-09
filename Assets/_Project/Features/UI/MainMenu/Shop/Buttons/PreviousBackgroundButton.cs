using _Project.Features.UI.Signals;


namespace _Project.Features.UI.MainMenu.Shop.Buttons
{
    public class PreviousBackgroundButton : BaseButton
    {
        protected override void OnClick()
        {
            _signalBus.Fire<PreviousBackgroundButtonClickedSignal>();
        }
    }
}