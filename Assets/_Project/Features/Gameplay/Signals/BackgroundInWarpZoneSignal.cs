using _Project.Features.Gameplay.Background;

namespace _Project.Features.Gameplay.Signals
{
    public class BackgroundInWarpZoneSignal
    {
        public BackgroundComponent backgroundToWarp;

        public BackgroundInWarpZoneSignal(BackgroundComponent backgroundToWarp)
        {
            this.backgroundToWarp = backgroundToWarp;
        }
    }
}