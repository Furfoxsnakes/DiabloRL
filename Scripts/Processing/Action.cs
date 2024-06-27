using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloRL.Scripts.Cartography.Tiles;

namespace DiabloRL.Scripts.Processing;

public abstract partial class Action {
    
    /// <summary>
    /// Wraps the Action in an ActionResult which allows for an alternate action from the Action's process
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public static implicit operator ActionResult(Action action) => new ActionResult(action);

    private DiabloEntity _diabloEntity;
    public DiabloEntity DiabloEntity => _diabloEntity;

    private Queue<Action> _actions;

    public Action(DiabloEntity entity) {
        _diabloEntity = entity;
    }

    public ActionResult Process(Queue<Action> actions) {
        _actions = actions;
        var result = OnProcess();
        _actions = null;

        return result;
    }

    protected abstract ActionResult OnProcess();

    public virtual void AfterSuccess() {
        if (DiabloEntity == null) return;
        
        DiabloEntity.Energy.Spend();
    }
}