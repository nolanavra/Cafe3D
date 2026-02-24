using System;
using UnityEngine;

namespace Cafe3D.Core.Utils
{
    public interface IPlayerInputSource
    {
        Vector2 Move { get; }
        Vector2 Look { get; }
        bool SprintHeld { get; }
        Vector2 Pan { get; }
        float Zoom { get; }
        Vector2 PointerPosition { get; }

        event Action InteractPressed;
        event Action CancelPressed;
        event Action OpenJournalPressed;
        event Action PausePressed;
        event Action PointerClickPressed;
        event Action PointerAltClickPressed;
        event Action RotatePartPressed;
        event Action DeletePressed;
        event Action ConfirmPressed;
        event Action ToggleWireToolPressed;
        event Action TogglePipeToolPressed;
        event Action RunDiagnosticsPressed;
        event Action TestCalibratePressed;

        void SetMode(GameInputMode mode);
    }

    public enum GameInputMode
    {
        Cafe = 0,
        Repair = 1
    }
}
