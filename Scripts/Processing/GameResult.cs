using System;
using DiabloRL.Scripts.Cartography.Tiles;

namespace DiabloRL.Scripts.Processing;

[Flags]
public enum GameResultFlags {
    Default         = 0x0000,
    NeedsPause      = 0x0001,
    NeedsUserInput  = 0x0002,
    CheckForCancel  = 0x0004,
    GameOver        = 0x0080
}

public partial class GameResult {
    private GameResultFlags _flags;
    
    private DiabloEntity _entity;
    public DiabloEntity Entity => _entity;

    public GameResult(GameResultFlags flags, DiabloEntity entity = null) {
        _flags = flags;
        _entity = entity;
    }

    public GameResult() : this(GameResultFlags.Default) {
        
    }

    private bool IsFlagSet(GameResultFlags flags) => (_flags & flags) == flags;

    public bool NeedsPause => IsFlagSet(GameResultFlags.NeedsPause);
    public bool NeedsAction => IsFlagSet(GameResultFlags.NeedsUserInput);
    public bool CheckForCancel => IsFlagSet(GameResultFlags.CheckForCancel);
    public bool IsGameOver => IsFlagSet(GameResultFlags.GameOver);
}