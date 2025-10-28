using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndState : GameState
{
    public GameEndState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        Time.timeScale = 0;
        UIManager.Instance.SetActiveGameEndPanel(true);

        if(Context.resultEndGame == ResultEndGame.Winner)
        {
            UIManager.Instance.SetTextResultEndGame("WINNER");
        }
        else if(Context.resultEndGame == ResultEndGame.Loser)
        {
            UIManager.Instance.SetTextResultEndGame("LOSER");
        }
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        UIManager.Instance.SetActiveGameEndPanel(false);
    }

    public override GameStateMachine.EState GetNextState()
    {
        if (Context.isRestartGame)
        {
            Context.stateMachine.RestartGame();
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
