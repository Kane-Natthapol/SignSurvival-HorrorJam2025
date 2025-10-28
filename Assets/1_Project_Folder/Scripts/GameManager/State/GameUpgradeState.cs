using UnityEngine;

public class GameUpgradeState : GameState
{
    public GameUpgradeState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        UIManager.Instance.SetActiveUpgradePanel(true);
    }
    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Context.isSelectedUppgrade = false;
        Context.isReWave = true;

        UIManager.Instance.SetActiveUpgradePanel(false);
    }

    public override GameStateMachine.EState GetNextState()
    {
        if(Context.isSelectedUppgrade)
        {
            return GameStateMachine.EState.GamePlay;
        }
        
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
