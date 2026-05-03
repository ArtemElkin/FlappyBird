using _Project.Core.Infrastructure.Save;

namespace _Project.Core.Data
{
    public class PlayerProgress : ISave
    {
        public int maxScore = 0;
        public int gold = 0;
    }
}