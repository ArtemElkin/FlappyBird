using _Project.Core.Signals;


namespace _Project.Features.UI.MainMenu
{
    public class PlayButton : BaseButton
    {
        protected override void OnClick()
        {
            _signalBus.Fire<StartGameClickedSignal>();
        }
    }
}
