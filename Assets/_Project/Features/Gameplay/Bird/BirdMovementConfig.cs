using _Project.Core.Infrastructure.Config;


namespace _Project.Features.Gameplay.Bird
{
    public class BirdMovementConfig : IConfig
    {
        public float minRotationZ;
        public float maxRotationZ;
        public float minVelocityY;
        public float maxVelocityY;
        public float jumpForce;
        public float glideSpeed;
        public float glideAmount;
    }
}