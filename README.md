# Voltage

A Unity VR horror prototype set inside an old apartment.  
The player is an electrician who enters the apartment to fix an electrical issue, triggers a disturbing supernatural event, loses power, and must restore electricity while exploring the space.

## Project Summary

This project is built in **Unity** using:

- **URP (Universal Render Pipeline)**
- **XR Interaction Toolkit**
- scripted interaction systems for doors, keys, fuse box logic, flashlight, and scare events

The current prototype already contains a playable progression loop and a first horror sequence.

## Current Gameplay Loop

1. Player enters the apartment
2. Entrance horror event triggers
3. Creature appears briefly
4. Lights fail
5. Flashlight becomes important
6. Player finds a key
7. Player unlocks and opens a locked door
8. Player finds a missing fuse
9. Player inserts the fuse into the fuse box
10. Power returns

## Implemented Features

### Environment
- apartment layout built and decorated
- props placed across rooms
- room lighting added and tuned
- fake city background outside windows
- atmosphere-focused material and texture setup

### VR Systems
- XR Interaction Toolkit setup
- trigger-based interactions
- hand/controller trigger detection
- stable scripted interactions preferred over physics-heavy solutions

### Flashlight
- working spotlight beam
- local glow / visibility in darkness
- used as the main light source after blackout

### Doors
- custom scripted transform-based doors
- support for:
  - open / close
  - delayed auto-close
  - sound playback
  - forced close and lock for scripted events

### Key System
- trigger-based key pickup
- progression flag storage via `GameProgress`
- locked door opens only after key is collected

### Fuse System
- fuse pickup object in apartment
- fuse box trigger slot
- fuse insertion logic
- static inserted fuse visual
- room lights restore after insertion

### Horror Sequence
- entrance trigger event
- creature appearance
- creature animation
- sound effects
- light flicker using intensity
- blackout sequence
- entrance door can slam shut and lock

## Important Technical Notes

### URP Lighting
The following settings are important for the project to work correctly:

- **Additional Lights** = `Per Pixel`
- **Per Object Limit** = `8` or `16`

If `Per Object Limit` is too low, lights may appear or disappear depending on view angle.

### Camera
Recommended clip settings:

- **Near Clip Plane** = `0.1`
- **Far Clip Plane** = `100`

Very small near clip values caused visual instability and z-fighting.

### Materials
Use:

- `Universal Render Pipeline/Lit` for objects that must react to light
- avoid `Unlit` on walls, floors, and props that should be illuminated by flashlight or room lights

## Main Scripts

### `SimpleDoor.cs`
Handles:
- open / close rotation
- auto close
- door sounds
- forced close and lock behavior

### `GameProgress.cs`
Singleton storing progression flags such as:
- `hasHallKey`

### `KeyPickup.cs`
Handles:
- trigger-based key pickup
- progression update
- hiding the collected key

### `LockedDoorHandle.cs`
Handles:
- locked door logic
- checks if player has the key before opening

### `FuseBoxSlot.cs`
Handles:
- fuse detection
- disabling original fuse object
- showing inserted fuse
- restoring light state

### `EntranceScareTrigger.cs`
Handles:
- entrance horror trigger
- creature appearance
- flickering lights
- blackout timing
- audio playback
- scripted scare sequence

## Recommended Unity / Git Setup

Make sure the project uses:

- `Version Control = Visible Meta Files`
- `Asset Serialization = Force Text`

Only commit:

- `Assets/`
- `Packages/`
- `ProjectSettings/`

Do **not** commit:

- `Library/`
- `Temp/`
- `Logs/`
- `UserSettings/`

## Current Project Status

The project is currently in a **playable prototype state**.

Already working:
- apartment environment
- room lighting
- flashlight
- door interactions
- key progression
- fuse progression
- first horror event
- creature animation and audio
- blackout and power restore flow

## Suggested Next Steps

- improve ambient audio and sound mixing
- add more environmental storytelling
- add another horror event after power returns
- add notes, clues, or optional interactions
- polish timing and pacing
- optimize lighting and colliders where needed

## One-Line Summary

A VR apartment horror prototype where the player enters, triggers a scare, loses power, uses a flashlight, finds a key, restores electricity with a fuse, and progresses through a scripted atmospheric horror sequence.
