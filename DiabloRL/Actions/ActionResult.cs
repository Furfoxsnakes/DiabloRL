using System;
using DiabloRL.Enums;

namespace DiabloRL.Actions
{
    [Flags]
    public enum ActionResultFlags
    {
        Default = 0x0000,
        Done    = 0x0001,
        NeedsPause  = 0x0002,
        Failed      = 0x0004,
        CheckForCancel  = 0x0008
    }

    public class ActionResult
    {
        public ActionResultFlags Flags => _flags;
        private ActionResultFlags _flags;
        
        public bool Success => !IsFlagSet(ActionResultFlags.Failed);
        public bool NeedsPause => IsFlagSet(ActionResultFlags.NeedsPause);
        public bool IsDone => IsFlagSet(ActionResultFlags.Done);
        public bool NeedsCheckForCancel => IsFlagSet(ActionResultFlags.CheckForCancel);

        /// <summary>
        /// Gets an alternate action that should be attempted when the previous action is not valid
        /// </summary>
        public Action Alternate => _alternate;
        private Action _alternate;
        
        /// <summary>
        /// Gets a typical ActionResult for an Action that is done and doesn't need a pause
        /// </summary>
        public static ActionResult Done => new ActionResult(ActionResultFlags.Done);
        
        /// <summary>
        /// Gets a typical ActionResult for an action that is done and DOES need a pause
        /// </summary>
        public static ActionResult DoneAndPause =>
            new ActionResult(ActionResultFlags.Done | ActionResultFlags.NeedsPause);
        
        /// <summary>
        /// Gets a typical ActionResult for an Action that is still going
        /// </summary>
        public static ActionResult NotDone => new ActionResult(ActionResultFlags.Default);

        /// <summary>
        /// Gets a typical ActionResult for an action that failed to complete
        /// </summary>
        public static ActionResult Fail => new ActionResult(ActionResultFlags.Failed | ActionResultFlags.Done);

        /// <summary>
        /// Gets a typical ActionResult for an Action that needs to check for a cancel
        /// </summary>
        public static ActionResult CheckForCancel =>
            new ActionResult(ActionResultFlags.CheckForCancel | ActionResultFlags.Done);

        public ActionResult(ActionResultFlags flags)
        {
            _flags = flags;
        }

        public ActionResult(Action alternateAction)
        {
            _alternate = alternateAction;
        }
        
        /// <summary>
        /// Checks flags on ActionResult
        /// </summary>
        public bool IsFlagSet(ActionResultFlags flags) => (_flags & flags) == flags;
    }
}