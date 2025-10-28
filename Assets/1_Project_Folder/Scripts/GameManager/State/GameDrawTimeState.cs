using UnityEngine;

public class GameDrawTimeState : GameState
{
    public GameDrawTimeState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        GameManager.Instance.SetIsRunTime(true);
    }

    public override void UpdateState()
    {
        if (GameManager.Instance.CheckTime() <= 0f)
        {
            Context.resultEndGame = ResultEndGame.Loser;
            Context.isStartEndGame = true;
        }
    }

    public override void ExitState()
    {
        GameManager.Instance.SetIsRunTime(false);
        Context.isStartResult = false;
        Context.isStartEndGame = false;
    }

    public override GameStateMachine.EState GetNextState()
    {
        if(Context.isStartResult)
        {
            return GameStateMachine.EState.ShowResult;
        }
        else if(Context.isStartEndGame)
        {
            return GameStateMachine.EState.ShowResult;
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
