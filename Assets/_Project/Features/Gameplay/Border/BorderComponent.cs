using UnityEngine;

namespace _Project.Features.Gameplay.Border
{
    public class BorderComponent : MonoBehaviour
    {
        private BoxCollider2D _col;


        private void Awake()
        {
            _col = GetComponent<BoxCollider2D>();
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Entered Border");
        }
    }
}
