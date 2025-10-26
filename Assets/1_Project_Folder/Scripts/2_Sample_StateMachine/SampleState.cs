using UnityEngine;

public abstract class SampleState : BaseState<SampleStateMachine.EState>
{
    protected SampleStateContext Context;

    public SampleState(SampleStateContext context, SampleStateMachine.EState stateKey) : base(stateKey)
    {
        Context = context;
    }
}
