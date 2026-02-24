using Cafe3D.Core.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe3D.Core.Bootstrap
{
    public sealed class GameModeController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Camera cafeCamera;
        [SerializeField] private Camera repairCamera;
        [SerializeField] private GameObject cafeUiRoot;
        [SerializeField] private GameObject repairUiRoot;
        [SerializeField] private UnityEngine.MonoBehaviour inputSourceBehaviour;

        private IPlayerInputSource inputSource;

        private void Awake()
        {
            inputSource = inputSourceBehaviour as IPlayerInputSource;
            EnterCafeMode();
        }

        public void EnterCafeMode()
        {
            playerInput.SwitchCurrentActionMap("Cafe");
            inputSource?.SetMode(GameInputMode.Cafe);
            SetPresentation(cafeEnabled: true);
        }

        public void EnterRepairMode()
        {
            playerInput.SwitchCurrentActionMap("Repair");
            inputSource?.SetMode(GameInputMode.Repair);
            SetPresentation(cafeEnabled: false);
        }

        private void SetPresentation(bool cafeEnabled)
        {
            if (cafeCamera != null) cafeCamera.enabled = cafeEnabled;
            if (repairCamera != null) repairCamera.enabled = !cafeEnabled;
            if (cafeUiRoot != null) cafeUiRoot.SetActive(cafeEnabled);
            if (repairUiRoot != null) repairUiRoot.SetActive(!cafeEnabled);
        }
    }
}
