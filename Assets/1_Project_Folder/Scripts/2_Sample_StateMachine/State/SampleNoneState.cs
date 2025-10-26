using UnityEngine;

public class SampleNoneState : SampleState
{
    public SampleNoneState(SampleStateContext context, SampleStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {

    }
    public override void UpdateState()
    {
    }

    public override void ExitState()
    {

    }

    public override SampleStateMachine.EState GetNextState()
    {
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
    {

    }

    public override void OnTriggerStay(Collider other)
    {

    }
}
