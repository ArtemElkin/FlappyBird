using _Project.Features.Gameplay.Background;


namespace _Project.Features.Gameplay.Signals
{
    public class BackgroundInWarpZoneSignal
    {
        public BackgroundLayerComponent BackgroundLayerToWarp;

        
        public BackgroundInWarpZoneSignal(BackgroundLayerComponent backgroundLayerToWarp)
        {
            this.BackgroundLayerToWarp = backgroundLayerToWarp;
        }
    }
}