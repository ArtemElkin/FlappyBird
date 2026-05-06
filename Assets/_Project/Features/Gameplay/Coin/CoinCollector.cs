using System;
using _Project.Core.Data;
using _Project.Features.Gameplay.Signals;
using Zenject;

namespace _Project.Features.Gameplay.Coin
{
    public class CoinCollector : IInitializable, IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly CoinFactory _coinFactory;
        private readonly SignalBus _signalBus;

        public CoinCollector(PlayerModel playerModel, CoinFactory coinFactory, SignalBus signalBus)
        {
            _playerModel = playerModel;
            _coinFactory = coinFactory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CoinCollectedSignal>(OnGoldCollected);
        }
        
        private void OnGoldCollected(CoinCollectedSignal signal)
        {
            _playerModel.AddGold(1);
            _coinFactory.Release(signal.coinComponent);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CoinCollectedSignal>(OnGoldCollected);
        }
    }
}