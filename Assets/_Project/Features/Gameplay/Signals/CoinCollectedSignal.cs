using _Project.Features.Gameplay.Coin;

namespace _Project.Features.Gameplay.Signals
{
    public class CoinCollectedSignal
    {
        public readonly CoinComponent coinComponent;
        
        public CoinCollectedSignal(CoinComponent coinComponent) =>  this.coinComponent = coinComponent;
    }
}