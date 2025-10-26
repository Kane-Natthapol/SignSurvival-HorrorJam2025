using UnityEngine;

public class SampleStateMachine : StateManager<SampleStateMachine.EState>
{
    public enum EState
    {
        None
    }

    SampleStateContext _context;

    private void Awake()
    {
        _context = new SampleStateContext(this);

        InitializeStates();
    }

    private void InitializeStates()
    {
        States.Add(EState.None, new SampleNoneState(_context, EState.None));
        CurrentState = States[EState.None];
    }
}
