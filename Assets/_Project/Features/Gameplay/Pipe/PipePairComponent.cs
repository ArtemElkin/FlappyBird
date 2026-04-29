using UnityEngine;

namespace _Project.Features.Gameplay.Pipe
{
    public class PipePairComponent : MonoBehaviour
    {
        public void SetNewLocalPositionY(float newLocalPositionY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, newLocalPositionY, transform.localPosition.z);
        }
    }
}
