using UnityEngine;

public class GameSetupState : GameState
{
    public GameSetupState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        UIManager.Instance.SetActiveMainMenuPanel(true);
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        Context.isStartGame = false;
        Context.isReWave = true;

        UIManager.Instance.SetActiveMainMenuPanel(false);
    }

    public override GameStateMachine.EState GetNextState()
    {
        if (Context.isStartGame)
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
