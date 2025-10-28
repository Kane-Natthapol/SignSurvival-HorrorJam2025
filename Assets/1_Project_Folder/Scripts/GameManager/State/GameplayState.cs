using UnityEngine;

public class GameplayState : GameState
{
    public GameplayState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        if (Context.isReWave)
        {
            Context.isReWave = false;
            UIManager.Instance.SetTextEnemyDie($"Die : 0");
        }

        UIManager.Instance.SetActiveGamePlayPanel(true);
    }
    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) GameStateMachine.Instance.PauseGame();
    }

    public override void ExitState()
    {
        Context.isGameEnd = false;
        Context.isPauseGame = false;
        Context.isWaveEnd = false;
    }

    public override GameStateMachine.EState GetNextState()
    {
        if(Context.isGameEnd)
        {
            return GameStateMachine.EState.GameEnd;
        }
        else if (Context.isPauseGame)
        {
            return GameStateMachine.EState.GamePause;
        }
        else if(Context.isWaveEnd)
        {
            return GameStateMachine.EState.GameUpgrade;
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
