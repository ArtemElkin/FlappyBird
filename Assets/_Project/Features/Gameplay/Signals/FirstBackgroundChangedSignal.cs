using System.Collections.Generic;
using _Project.Features.Gameplay.Background;

namespace _Project.Features.Gameplay.Signals
{
    public class FirstBackgroundChangedSignal
    {
        public BackgroundLayerComponent NewFirstBackgroundLayer;
        public BackgroundLayerComponent PreviousFirstBackgroundLayer;

        public FirstBackgroundChangedSignal(
            BackgroundLayerComponent newFirstBackgroundLayer,
            BackgroundLayerComponent previousFirstBackgroundLayer)
        {
            this.NewFirstBackgroundLayer = newFirstBackgroundLayer;
            this.PreviousFirstBackgroundLayer = previousFirstBackgroundLayer;
        }
    }
}