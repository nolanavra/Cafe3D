# Sample Scene Assembly Checklist

1. Create/open scene `Assets/_Game/Unity/Scenes/SampleCafeScene.unity`.
2. Add a `PlayerRig` GameObject and attach:
   - `PlayerInput` component
   - `UnityPlayerInputSource`
   - `GameBootstrap`
3. Assign `GameInput.inputactions` to `PlayerInput.Actions`.
4. Set `PlayerInput.Default Action Map` to `Cafe` and behavior to `Invoke Unity Events` (or C# events).
5. Add `GameModeController` to a bootstrap object and wire:
   - `playerInput`
   - café camera + repair camera
   - café UI root + repair UI root
   - `inputSourceBehaviour` => `UnityPlayerInputSource`
6. Add `CafePlayerController` and `RepairScreenController`; point both at the same input source and command queue.
7. Add EventSystem with `InputSystemUIInputModule`.
8. Save the scene.
