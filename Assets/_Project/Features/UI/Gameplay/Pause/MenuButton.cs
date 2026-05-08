using _Project.Core.Signals;


namespace _Project.Features.UI.Gameplay.Pause
{
    public class MenuButton : BaseButton
    {
        protected override void OnClick()
        {
            _signalBus.Fire<MenuClickedSignal>();
        }
    }
}