using _Project.Core.Data;
using Zenject;

namespace _Project.Features.Gameplay.Gold
{
    public class GoldCollector
    {
        private PlayerModel _playerModel;

        [Inject]
        public GoldCollector(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void CollectGold(int amount)
        {
            _playerModel.AddGold(amount);
        }
    }
}