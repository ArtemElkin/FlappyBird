using System;
using UnityEngine;


namespace _Project.Core.Input
{
    public class InputHandler : MonoBehaviour, IInputService
    {
        public event Action JumpPressed;
        private void Update()
        {
            CheckJump();
        }

        private void CheckJump()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                JumpPressed?.Invoke();
            }
        }
    }
}
