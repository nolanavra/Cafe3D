using System;
using Cafe3D.Core.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe3D.Unity.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class UnityPlayerInputSource : MonoBehaviour, IPlayerInputSource
    {
        [SerializeField] private PlayerInput playerInput;

        private GameInputActions actions;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool SprintHeld { get; private set; }
        public Vector2 Pan { get; private set; }
        public float Zoom { get; private set; }
        public Vector2 PointerPosition { get; private set; }

        public event Action InteractPressed;
        public event Action CancelPressed;
        public event Action OpenJournalPressed;
        public event Action PausePressed;
        public event Action PointerClickPressed;
        public event Action PointerAltClickPressed;
        public event Action RotatePartPressed;
        public event Action DeletePressed;
        public event Action ConfirmPressed;
        public event Action ToggleWireToolPressed;
        public event Action TogglePipeToolPressed;
        public event Action RunDiagnosticsPressed;
        public event Action TestCalibratePressed;

        private void Awake()
        {
            if (playerInput == null)
            {
                playerInput = GetComponent<PlayerInput>();
            }

            actions = new GameInputActions(playerInput.actions);
            BindCafe();
            BindRepair();
        }

        private void OnEnable()
        {
            playerInput.actions.Enable();
        }

        private void OnDisable()
        {
            playerInput.actions.Disable();
        }

        public void SetMode(GameInputMode mode)
        {
            var mapName = mode == GameInputMode.Cafe ? "Cafe" : "Repair";
            playerInput.SwitchCurrentActionMap(mapName);
        }

        private void BindCafe()
        {
            var cafe = actions.Cafe;
            cafe.FindAction("Move", true).performed += ctx => Move = ctx.ReadValue<Vector2>();
            cafe.FindAction("Move", true).canceled += _ => Move = Vector2.zero;
            cafe.FindAction("Look", true).performed += ctx => Look = ctx.ReadValue<Vector2>();
            cafe.FindAction("Look", true).canceled += _ => Look = Vector2.zero;
            cafe.FindAction("Sprint", true).performed += _ => SprintHeld = true;
            cafe.FindAction("Sprint", true).canceled += _ => SprintHeld = false;
            cafe.FindAction("Interact", true).performed += _ => InteractPressed?.Invoke();
            cafe.FindAction("Cancel", true).performed += _ => CancelPressed?.Invoke();
            cafe.FindAction("OpenJournal", true).performed += _ => OpenJournalPressed?.Invoke();
            cafe.FindAction("Pause", true).performed += _ => PausePressed?.Invoke();
        }

        private void BindRepair()
        {
            var repair = actions.Repair;
            repair.FindAction("Pan", true).performed += ctx => Pan = ctx.ReadValue<Vector2>();
            repair.FindAction("Pan", true).canceled += _ => Pan = Vector2.zero;
            repair.FindAction("Zoom", true).performed += ctx => Zoom = ctx.ReadValue<float>();
            repair.FindAction("Zoom", true).canceled += _ => Zoom = 0f;
            repair.FindAction("PointerPosition", true).performed += ctx => PointerPosition = ctx.ReadValue<Vector2>();
            repair.FindAction("PointerClick", true).performed += _ => PointerClickPressed?.Invoke();
            repair.FindAction("PointerAltClick", true).performed += _ => PointerAltClickPressed?.Invoke();
            repair.FindAction("RotatePart", true).performed += _ => RotatePartPressed?.Invoke();
            repair.FindAction("Delete", true).performed += _ => DeletePressed?.Invoke();
            repair.FindAction("Confirm", true).performed += _ => ConfirmPressed?.Invoke();
            repair.FindAction("Cancel", true).performed += _ => CancelPressed?.Invoke();
            repair.FindAction("ToggleWireTool", true).performed += _ => ToggleWireToolPressed?.Invoke();
            repair.FindAction("TogglePipeTool", true).performed += _ => TogglePipeToolPressed?.Invoke();
            repair.FindAction("RunDiagnostics", true).performed += _ => RunDiagnosticsPressed?.Invoke();
            repair.FindAction("TestCalibrate", true).performed += _ => TestCalibratePressed?.Invoke();
        }
    }
}
