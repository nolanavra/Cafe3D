# Setup Notes

Deterministic defaults chosen where AGENTS.md was unspecified:

- Tick source for command timestamps uses a simple incrementing integer in controllers.
- Repair ghost placement currently snaps to `(0,0)` grid placeholder in scaffold code until full board picking is connected.
- Diagnostics solver is a deterministic stub with fixed pressure/temperature output and continuity checks based on node/edge presence.
- Save format version is `1.0.0` with JSON serialization through `JsonUtility`.
- Sample scene/prefabs are minimal Unity YAML placeholders intended to be opened and completed in the Unity Editor.
