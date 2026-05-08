using System.Collections.Generic;
using _Project.Features.Gameplay.Background;

namespace _Project.Features.Gameplay.Signals
{
    public class FirstBackgroundChangedSignal
    {
        public BackgroundComponent newFirstBackground;
        public BackgroundComponent previousFirstBackground;

        public FirstBackgroundChangedSignal(
            BackgroundComponent newFirstBackground,
            BackgroundComponent previousFirstBackground)
        {
            this.newFirstBackground = newFirstBackground;
            this.previousFirstBackground = previousFirstBackground;
        }
    }
}