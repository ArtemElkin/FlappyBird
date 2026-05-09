using _Project.Features.UI.Signals;


namespace _Project.Features.UI.MainMenu.Shop.Buttons
{
    public class NextBackgroundButton : BaseButton
    {
        protected override void OnClick()
        {
            _signalBus.Fire<NextBackgroundButtonClickedSignal>();
        }
    }
}