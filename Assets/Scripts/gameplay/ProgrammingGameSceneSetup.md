# Programming Game Scene Setup

This project now includes beginner-friendly block commands:
- `MoveForwardView`
- `TurnLeftView`
- `TurnRightView`

And gameplay helpers:
- `ProgrammingGameFlowController`
- `GoalZone`
- `FailZone`

## 1. Create the New Scene

1. Create a new scene (for example `ProgrammingLevel01`).
2. Add a ground plane and a visible player object (Cube/Capsule).
3. Add a goal object with a trigger collider.
4. Optionally add one or more fail zones (holes, lava, out-of-bounds box colliders set as triggers).

## 2. Bring in the Block UI Base

The fastest approach is to copy the block-coding UI hierarchy from `SampleScene`:
- `Canvas`
- block creator area/palette
- workspace root (`RectTransform`) where dropped commands are executed
- edit/game mode UI groups
- run/stop buttons
- `CommandsManager`

Then in the copied `CommandsManager`:
1. Set `target` to your player object.
2. Keep only the blocks you want in the `blocks` list.
3. Add the new block prefabs/views listed below.

## 3. Create the New Block Prefabs

Create three new command creator prefabs by duplicating an existing motion command prefab and changing the script component:

1. Duplicate `MoveByVectorView.prefab` three times.
2. Replace script with:
   - `MoveForwardView`
   - `TurnLeftView`
   - `TurnRightView`
3. Set text labels to user-friendly names like `move forward`, `turn left`, `turn right`.
4. Tune defaults in inspector:
   - `MoveForwardView.distance = 1`
   - `MoveForwardView.duration = 0.25`
   - `TurnLeftView.degrees = 90`
   - `TurnRightView.degrees = 90`
5. Add these prefabs to `CommandsManager.blocks`.

## 4. Wire Win/Lose Flow

1. Create an empty object `GameFlow` and add `ProgrammingGameFlowController`.
2. Assign `commandsManager` on `GameFlow`.
3. Optional: assign `winStateUi` and `failStateUi` GameObjects.
4. On your goal trigger object, add `GoalZone`:
   - Set `target` to the player transform (or tag player as `Player`).
   - In `onGoalReached`, add `GameFlow -> ProgrammingGameFlowController.HandleGoalReached`.
5. On each fail trigger object, add `FailZone`:
   - Set `target` to player transform (or tag player as `Player`).
   - In `onFailed`, add `GameFlow -> ProgrammingGameFlowController.HandleFailReached`.

## 5. Run/Stop Button Wiring

Point UI buttons to `ProgrammingGameFlowController` instead of direct `CommandsManager` calls:
- Run button -> `RunProgram()`
- Stop button -> `StopProgram()`

## 6. Test Scenario

A minimal first level test:
1. Place player at (0,0,0), facing +Z.
2. Place goal at (0,0,3).
3. Use blocks:
   - `move forward`
   - `move forward`
   - `move forward`
4. Run and verify goal trigger shows win state.
