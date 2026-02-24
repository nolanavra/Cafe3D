using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe3D.Core.Bootstrap
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        private void Awake()
        {
            playerInput.defaultActionMap = "Cafe";
            playerInput.SwitchCurrentActionMap("Cafe");
        }
    }
}
