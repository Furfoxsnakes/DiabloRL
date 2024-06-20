using System;

namespace DiabloRL.Scripts.Processing;

[Flags]
public enum ActionResultFlags {
    Default = 0x0000,
    Done = 0x0001,
    NeedsPause = 0x0002,
    Failed = 0x0004,
    CheckForCancel = 0x0008
}

public partial class ActionResult {
    
    private ActionResultFlags _flags;

    public Action AlternateAction => _alternateAction;
    private Action _alternateAction;

    public bool Success => !IsFlagSet(ActionResultFlags.Failed);
    public bool NeedsPause => IsFlagSet(ActionResultFlags.NeedsPause);
    public bool IsDone => IsFlagSet(ActionResultFlags.Done);
    public bool NeedsCheckForCancel => IsFlagSet(ActionResultFlags.CheckForCancel);
    
    public ActionResult(ActionResultFlags resultFlags) {
        _flags = resultFlags;
    }

    public ActionResult(Action alternateAction) {
        _alternateAction = alternateAction;
    }
    
    private bool IsFlagSet(ActionResultFlags flags) {
        return (_flags & flags) == flags;
    }

    public static ActionResult Done => new ActionResult(ActionResultFlags.Done);
    public static ActionResult DoneAndPause => new ActionResult(ActionResultFlags.Done | ActionResultFlags.NeedsPause);
    public static ActionResult NotDone => new ActionResult(ActionResultFlags.Default);
    public static ActionResult Fail => new ActionResult(ActionResultFlags.Failed);
    public static ActionResult CheckForCancel => new ActionResult(ActionResultFlags.CheckForCancel);
}