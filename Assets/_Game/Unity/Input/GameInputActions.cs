using UnityEngine.InputSystem;

namespace Cafe3D.Unity.Input
{
    // Placeholder for the generated wrapper from GameInput.inputactions.
    // Regenerate via Unity Input Actions importer when editing bindings.
    public sealed class GameInputActions
    {
        public InputActionAsset Asset { get; }

        public GameInputActions(InputActionAsset asset)
        {
            Asset = asset;
        }

        public InputActionMap Cafe => Asset.FindActionMap("Cafe", true);

        public InputActionMap Repair => Asset.FindActionMap("Repair", true);
    }
}
