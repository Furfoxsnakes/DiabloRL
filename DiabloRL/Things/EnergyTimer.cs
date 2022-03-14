using DiabloRL.Actions;

namespace DiabloRL.Things;

public class EnergyTimer
{
    private string _name;
    private Action _elapsedAction;
    private Action _stepAction;
    private bool _doesRepeat;
    private int _steps;
    private int _elapsed;

    public string Name => _name;

    public EnergyTimer(string name, Action elapsedAction, int steps, Action stepAction, bool doesRepeat)
    {
        _name = name;
        _elapsedAction = elapsedAction;
        _stepAction = stepAction;
        _doesRepeat = doesRepeat;
    }
}