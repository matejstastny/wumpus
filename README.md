<img src="./Assets/Sprites/screens/banner.png" alt="Repo banner" size="100%" />)

# Hunt the Wumpus

A 2D turn-based strategy game built in Unity, submitted for the 2025 Microsoft _Hunt the Wumpus_ event at Redmond High School.

**Team:**

- [Matej Stastny](https://github.com/matejstastny)
- [Samuel Thiophillus](https://github.com/Snapshot20)
- [Moby LaForge](https://github.com/MobyWonKenobi)
- [Alex Chang](https://github.com/alexchang319)

## Gameplay

Two players take turns moving pieces across a hexagonal grid. Move onto an enemy piece to capture it. A turn ends automatically once all of your pieces have moved.

- **Hexagonal grid** — 5×6 hex board
- **Drag and drop** movement
- **Capture** — land on an enemy piece to eliminate it
- **Movement range** — each piece has a configurable reach
- **Procedural maze** — rooms are connected by randomly generated doors

## Requirements

- Unity 2023.2

## Running the Project

1. Open the project folder in Unity Hub
2. Open `Assets/Scenes/Main.unity`
3. Press **Play**

## Project Structure

```
Assets/
  Scripts/
    GameController.cs   - Grid setup, input handling, turn management
    Tile.cs             - Hex tile logic, adjacency, door generation
    Piece.cs            - Unit state (movement, capture, side)
    EndTurn.cs          - End turn button
    UnitInfo.cs         - Selected unit HUD display
    WelcomeScreen.cs    - Start screen scaffolding
  Prefabs/              - Tile, piece, and door prefabs
  Scenes/Main.unity     - Main scene
  Sprites/              - Environment, player, and icon assets
  Materials/            - Player side color materials
```
