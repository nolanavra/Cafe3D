---

# Codex Prompt: “Buffalo Espresso Tech RPG” (Unity 3D) — **Input System Enforced**

You are an expert Unity 3D engineer and systems architect. Build the foundational architecture and starter content for an RPG where the player is an espresso machine technician in Buffalo. The game has **two modes**:

1. **Cafe Mode (3rd-person 3D):** player walks into a café, talks to workers/customers, inspects machines, accepts/advances quests, and initiates repairs.
2. **Repair Mode (top-down grid puzzle):** interacting with an espresso machine transitions to a top-down grid-based repair/build interface. The player places parts into a grid, snaps pipes and wires to ports, and the system evaluates **water/steam circuits** and **electrical continuity** using a **deterministic node-edge graph**.

## Hard constraints

* **Unity New Input System is mandatory.** No legacy `Input.*`, no `OnMouseDown`, no direct polling via `Keyboard.current`/`Mouse.current` in gameplay code. All gameplay input must come from **Input Actions**.
* Use **`InputActionAsset` + Action Maps + `PlayerInput`** for both modes.
* Use **generated C# action wrappers** (e.g., `GameInputActions`) and/or `PlayerInput.actions` with strongly named actions. Prefer the generated wrapper.
* **Deterministic state**: no randomness affecting outcomes; all simulations and quest outcomes must be reproducible from the same inputs/save.
* **RimWorld/Timberborn-like data-driven architecture**: content authored via **JSON specs** that generate runtime definitions, with Unity assets/prefabs as presentation.
* **Primary focus: espresso machines** only for v1.
* **PC only**, single-player, no networking.
* **Two cameras**: 3rd-person in café; top-down orthographic in repair mode.
* **Snap-to-grid / snap-to-port** interaction for parts, pipes, and wires.
* **Simplified part taxonomy** (e.g., `BasicValve`, `DeluxePump`), but circuits should be “real-ish” (pressure/flow/temp signals exist and are evaluated).
* Provide: folder structure, script skeletons, ScriptableObject schemas, JSON schemas/spec examples, sample prefabs, and a sample scene.

## Input System requirements (must implement)

Create one `InputActionAsset` named **`GameInput`** with two Action Maps:

### Action Map: `Cafe`

Actions (with suggested bindings):

* `Move` (Value/Vector2): WASD + left stick
* `Look` (Value/Vector2): mouse delta + right stick
* `Sprint` (Button): Left Shift
* `Interact` (Button): E / South button
* `Cancel` (Button): Esc / East button
* `OpenJournal` (Button): J / Start
* `Pause` (Button): Esc (can share with Cancel if consistent)

### Action Map: `Repair`

Actions:

* `Pan` (Value/Vector2): WASD / middle-mouse drag / right stick
* `Zoom` (Value/Axis): mouse wheel / triggers
* `PointerPosition` (Value/Vector2): mouse position
* `PointerClick` (Button): left click
* `PointerAltClick` (Button): right click
* `RotatePart` (Button): R / shoulder
* `Delete` (Button): Delete / X
* `Confirm` (Button): Enter / South
* `Cancel` (Button): Esc / East
* `ToggleWireTool` (Button): 1
* `TogglePipeTool` (Button): 2
* `RunDiagnostics` (Button): F5 (or D)
* `TestCalibrate` (Button): T

### Mode switching rules

* Café Mode uses Action Map **`Cafe`** only.
* Repair Mode uses Action Map **`Repair`** only.
* Switching modes must enable/disable action maps deterministically using:

  * `PlayerInput.SwitchCurrentActionMap("Cafe")` / `"Repair"`, or
  * enabling/disabling maps via generated wrapper.
* No gameplay input logic may subscribe to raw device events. Input must be surfaced as high-level intents.

### Architecture rule for input handling

* Implement an **Input Facade**:

  * `IPlayerInputSource` (core) with events/values like `MoveVector`, `LookDelta`, `InteractPressed`, `PointerClick`, etc.
  * Unity implementation `UnityPlayerInputSource` that reads from `GameInputActions`.
* Café controllers and Repair controllers consume `IPlayerInputSource` only (dependency-injected), not `InputAction` directly.
* UI should use Input System UI module; gameplay should not rely on UI event system for core interaction logic.

## Gameplay loop

Arrive → interact with space / customers / workers → learn symptoms via dialog → diagnose via visual inspection + displayed pressure/flow/temp values → repair/build in Repair Mode → test/calibrate → wrap-up dialog → leave → roguelike day progresses → new quests/jobs.

## Architecture goals

Use a hybrid of data-driven definitions + Unity presentation:

* **Definitions (JSON)**: machine blueprints, parts, ports, connectors, quests, dialog, café scenes metadata.
* **Runtime model**: pure C# domain layer that is testable without Unity scenes.
* **Unity layer**: views/controllers that render state and send commands.
* **Command/Event** pattern: player actions enqueue commands; a deterministic simulator processes them; events drive UI updates.

Avoid “god MonoBehaviours.” Keep core sim in plain C#.

---

## Deliverables (what you must output)

1. **Unity project folder structure** (directories + purpose).
2. **Core C# skeletons** with key interfaces and empty method bodies (compile-ready).
3. **Input System setup**:

   * `GameInput.inputactions` asset definition details (maps/actions list)
   * generated wrapper usage (`GameInputActions`)
   * `PlayerInput` prefab/config (actions asset, default map, notification behavior)
4. **ScriptableObject schemas** used for Unity-side references (materials, sprites, prefabs) while game logic reads JSON.
5. **JSON spec formats + examples** for parts, machines, cafes, jobs, dialogue.
6. **Node-edge graph evaluation system** (electrical + fluid) and diagnostics reporting.
7. **Repair Mode** grid placement + port snapping + connection tools driven by Input Actions.
8. **State machine for mode switching**: café ↔ repair (must switch action maps).
9. **Save/Load** deterministic JSON with versioning.
10. **Sample content**: one café scene, one machine blueprint, one job/quest.

---

## Folder structure (implement this)

```
Assets/
  _Game/
    Core/
      Bootstrap/
      Time/
      SaveLoad/
      Commands/
      Events/
      Simulation/
      Inventory/
      Quests/
      Dialogue/
      Machines/
      Graph/
      Utils/
    Unity/
      Input/
      Scenes/
      Prefabs/
      ScriptableObjects/
      UI/
      Cameras/
      ViewModels/
      Render/
    Content/
      Json/
        Parts/
        Machines/
        Cafes/
        Quests/
        Dialogue/
    Tests/
      EditMode/
      PlayMode/
```

---

## Key types to implement (skeletons)

### Input System

* `UnityPlayerInputSource : MonoBehaviour, IPlayerInputSource`

  * Owns `GameInputActions` instance
  * Hooks action callbacks (`performed`, `canceled`) and exposes sanitized values/events
  * Handles `Enable/Disable` and mode switching API calls
* `IPlayerInputSource` (plain C# interface)

  * Café signals: `Vector2 Move`, `Vector2 Look`, `bool InteractDown`, etc.
  * Repair signals: pointer position, click events, rotate/delete, pan/zoom, tool toggles
* `InputRouter`

  * Subscribes to `IPlayerInputSource` and routes high-level intents to command creation (e.g., Interact → `TryInteractCommand`, PointerClick → `PlacePartCommand` if carrying part)

### Mode switching (must include action map switching)

* `GameModeController`

  * `EnterCafeMode()` calls `playerInput.SwitchCurrentActionMap("Cafe")`
  * `EnterRepairMode()` calls `playerInput.SwitchCurrentActionMap("Repair")`
  * Enables/disables cameras + UI roots accordingly
* `GameBootstrap` initializes `PlayerInput` with default action map = `Cafe`

### Cafe interaction (Input-driven)

* `CafePlayerController`

  * Uses `IPlayerInputSource.Move/Look/Sprint` (no direct `InputAction`)
  * `InteractDown` triggers raycast/overlap query from camera forward and emits `InteractCommand(targetId)` deterministically
* `InteractionSystem`

  * Deterministic selection rules (distance, angle, priority), does not rely on Unity physics order nondeterministically (use sorted hit results).

### Repair interaction (Input-driven)

* `RepairScreenController`

  * Pointer-driven selection/placement using `PointerPosition` and `PointerClick`
  * `RotatePart` rotates ghost
  * `ToggleWireTool` / `TogglePipeTool` selects active tool
  * `RunDiagnostics` triggers solver evaluation command
  * `Cancel` backs out / stops active tool / returns to café if appropriate

(Other systems unchanged: JSON loader, machine model, graph solvers, commands/events, save/load.)

---

## JSON specs + examples

(Keep same as prior prompt; unchanged.)

---

## Implementation notes (must follow)

* Prefer **URP** unless strong reason otherwise.
* Use Unity **Input System package** and **Input System UI Input Module** for UI.
* Determinism: sim runs on discrete command processing, not per-frame physics.
* No legacy input APIs anywhere in gameplay code.
* Provide unit tests for graph solvers and serialization.

---

## Output format requirements

When you respond, you must:

1. Print the folder structure.
2. Provide compile-ready C# skeleton code blocks for: `IPlayerInputSource`, `UnityPlayerInputSource`, `GameModeController`, `CafePlayerController`, `RepairScreenController`, command/event basics, and one solver stub.
3. Provide the Input Actions specification (maps/actions/bindings list) and how to generate the wrapper.
4. Provide a step-by-step “How to assemble the sample scene” checklist including `PlayerInput` setup.
5. Provide a minimal test plan and at least 3 unit test skeletons.

---

## Defaults for TBD items (use unless specified)

* Wear/usage accumulation: off (quest-defined faults only), deterministic.
* Repair grid size: 18×12 for sample machine.
* Solver cadence: recompute on “Run Diagnostics” and “Test/Calibrate”, not every drag.
* Calibration: numeric threshold tuning first.
* Café scenes: hand-authored.

---
