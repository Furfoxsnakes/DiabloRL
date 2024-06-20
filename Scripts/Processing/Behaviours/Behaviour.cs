namespace DiabloRL.Scripts.Processing.Behaviours;

public abstract partial class Behaviour {
    public virtual bool NeedsUserInput => false;

    public abstract Action NextAction();

    public virtual void Disturb() { }
    
    public virtual void Cancel() { }
}