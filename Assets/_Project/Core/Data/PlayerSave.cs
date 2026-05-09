using System.Collections.Generic;
using _Project.Core.Infrastructure.Save;


namespace _Project.Core.Data
{
    public class PlayerSave : ISave
    {
        public int maxScore = 0;
        public int coins = 0;
        public int currentBackgroundId = 0;
        public List<int> unlockedBackgroundIds = new() {0};
    }
}