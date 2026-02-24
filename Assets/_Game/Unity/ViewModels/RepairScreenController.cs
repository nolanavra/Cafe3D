using Cafe3D.Core.Commands;
using Cafe3D.Core.Utils;
using UnityEngine;

namespace Cafe3D.Unity.ViewModels
{
    public sealed class RepairScreenController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour inputSourceBehaviour;
        [SerializeField] private CommandQueue commandQueue;

        private IPlayerInputSource inputSource;
        private int ghostRotation;
        private int tick;
        private string activeTool = "Pipe";

        private void Awake()
        {
            inputSource = inputSourceBehaviour as IPlayerInputSource;
        }

        private void OnEnable()
        {
            if (inputSource == null)
            {
                return;
            }

            inputSource.PointerClickPressed += OnPointerClick;
            inputSource.RotatePartPressed += OnRotatePart;
            inputSource.ToggleWireToolPressed += () => activeTool = "Wire";
            inputSource.TogglePipeToolPressed += () => activeTool = "Pipe";
            inputSource.RunDiagnosticsPressed += OnRunDiagnostics;
            inputSource.CancelPressed += OnCancel;
        }

        private void OnDisable()
        {
            if (inputSource == null)
            {
                return;
            }

            inputSource.PointerClickPressed -= OnPointerClick;
            inputSource.RotatePartPressed -= OnRotatePart;
            inputSource.RunDiagnosticsPressed -= OnRunDiagnostics;
            inputSource.CancelPressed -= OnCancel;
        }

        private void Update()
        {
            tick++;
            _ = inputSource != null ? inputSource.PointerPosition : Vector2.zero;
            _ = inputSource != null ? inputSource.Pan : Vector2.zero;
            _ = inputSource != null ? inputSource.Zoom : 0f;
        }

        private void OnPointerClick()
        {
            if (commandQueue == null)
            {
                return;
            }

            var gridPos = new Vector2Int(0, 0);
            commandQueue.Enqueue(new PlacePartCommand(tick, activeTool + "Ghost", gridPos, ghostRotation));
        }

        private void OnRotatePart()
        {
            ghostRotation = (ghostRotation + 1) % 4;
        }

        private void OnRunDiagnostics()
        {
        }

        private void OnCancel()
        {
        }
    }
}
