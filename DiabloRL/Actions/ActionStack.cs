using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Actions;
using SadConsole.Components;
using SadConsole.Input;

namespace DiabloRL.Actions;

public class ActionStack : Stack<SadConsole.Actions.ActionBase>, IComponent
{
    // implement with sadconsole 9.x components
    //https://github.com/thesadrogue/SadConsole.GoRogueHelpers/blob/master/src/Actions/ActionStack.cs
    
    public uint SortOrder => _componentImplementation.SortOrder;

    public bool IsUpdate => _componentImplementation.IsUpdate;

    public bool IsRender => _componentImplementation.IsRender;

    public bool IsMouse => _componentImplementation.IsMouse;

    public bool IsKeyboard => _componentImplementation.IsKeyboard;
    private IComponent _componentImplementation;

    /// <summary>
    /// Pushes the action to the stack and immediately calls Run
    /// </summary>
    /// <param name="action"></param>
    /// <param name="timeElapsed"></param>
    public void PushAndRun(ActionBase action, TimeSpan timeElapsed = default)
    {
        if (!action.IsFinished)
        {
            if (Count == 0 || Peek() != action)
                Push(action);

            action.Run(timeElapsed);
        }
    }

    /// <summary>
    /// Removes all finished actions from the top of the stack and runs the action on the top
    /// </summary>
    /// <param name="timeElapsed"></param>
    public void Run(TimeSpan timeElapsed)
    {
        if (Count == 0) return;

        while (Count != 0 && Peek().IsFinished)
            Pop();

        if (Count != 0)
            Peek().Run(timeElapsed);

        while (Count != 0 && Peek().IsFinished)
            Pop();
    }

    public void Update(IScreenObject host, TimeSpan delta) => Run(delta);

    public void Render(IScreenObject host, TimeSpan delta) => throw new NotImplementedException();

    public void ProcessMouse(IScreenObject host, MouseScreenObjectState state, out bool handled) => throw new NotImplementedException();

    public void ProcessKeyboard(IScreenObject host, Keyboard keyboard, out bool handled) => throw new NotImplementedException();

    public void OnAdded(IScreenObject host) { }

    public void OnRemoved(IScreenObject host) { }
    
}