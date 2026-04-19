# HexBlobs - A Multiplayer Turn-Based Strategy Game

HexBlobs is a simple to understand two-player turn-based strategy game with lots depth and tactics. Features include multiple move types on a hex-grid map, persistent match history, and multiple map sizes to choose from.

## Setup

### Prerequisites
- Unity 2022.3 LTS or later
- Git

### Installation

1. Clone this repository:
```bash
git clone https://github.com/AaronM-at0m1c/HexBlobs/
```
3. Open the project in Unity Hub
4. Open the MainMenu scene in Assets/Scenes/
5. Press Play to test in the editor

## How to Play

### Controls
1. Left Click to place tiles and interact with Menu buttons
2. Thats it! It is very easy.

### Objective
Win control of the board vs. the other player. The player with the most tiles at the end of the game wins. The game ends when either there are no tiles left or when one player has no valid moves remaining.

## Testing Multiplayer
1. Open ParrelSync → Clones Manager
2. Create a clone of the project
3. Open the clone in a separate Unity Editor
4. Run both editors simultaneously
5. In one instance, start as Host in NetworkManager
6. In the other instance, start as Client in NetworkManager

## Technical Implementation
**Singleton Pattern**
- Location: Assets/Scripts/GameManager.cs
- Description: Manages game state

**Delegate**
- Location: Assets/Scripts/GameManager.cs
- Description: OnBoardChanged notifies that the board has updated. This is subscribed to by UIManager.

**Command Pattern**
- Location: Assets/Scripts/FlipCommand.cs | Assets/Scripts/JumpCommand.cs
- Description: Each move encapsulated as a command object

## Known Issues
- Jump/Flip/Invalid move logic is currently imperfect. As a result, occasionally moves behave in unexpected ways.
- Large map is currently incomplete, though it just needs to be wired up and it should work fine.

## Future Enhancements
- Removal of current move bugs
- Implent new "special" moves
- Finish larger map implementation

## Technologies Used

- **Unity 2022.3 LTS**: Game engine
- **Netcode for GameObjects**: Multiplayer networking via RPC
- **SQLite**: Database for persistent storage
- **TextMeshPro**: UI text rendering
