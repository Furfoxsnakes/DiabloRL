using System;
using SadRogue.Integration;

namespace DiabloRL.Processing
{
    [Flags]
    public enum GameResultFlags
    {
        Default =       0x0001,
        NeedsPause =    0x0002,
        NeedsUserInput = 0x0004,
        CheckForCancel = 0x0008,
        GameOver =      0x00016
    }

    public class GameResult
    {

        public bool NeedsPause => IsFlagSet(GameResultFlags.NeedsPause);
        public bool NeedsUserInput => IsFlagSet(GameResultFlags.NeedsUserInput);
        public bool CheckForCancel => IsFlagSet(GameResultFlags.CheckForCancel);
        public bool IsGameOver => IsFlagSet(GameResultFlags.GameOver);

        public RogueLikeEntity Entity => _entity;

        public GameResult(GameResultFlags flags, RogueLikeEntity entity)
        {
            _flags = flags;
            _entity = entity;
        }

        public GameResult(GameResultFlags flags) : this(flags, null)
        {
        }

        public GameResult() : this(GameResultFlags.Default)
        {
            
        }

        private bool IsFlagSet(GameResultFlags flags) => (_flags & flags) == flags;

        private GameResultFlags _flags;
        private RogueLikeEntity _entity;
        
    }
}