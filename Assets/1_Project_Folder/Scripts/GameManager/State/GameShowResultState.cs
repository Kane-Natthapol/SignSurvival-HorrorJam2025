using UnityEngine;

public class GameShowResultState : GameState
{
    public GameShowResultState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        if(Context.resultEndGame == ResultEndGame.Loser)
        {
            HandManager.Instance.StartHandCutOffAll(GameManager.Instance.CheckHealthPlayer());
            //Context.isStartEndGame = true;
            Context.waitNextState = WaitNextState.NextStartEndGame;
        }
        else
        {
            if (!Context.isPass)
            {
                GameManager.Instance.AddHealthPlayer(1);

                if (GameManager.Instance.CheckHealthPlayer() == 0)
                {
                    Context.resultEndGame = ResultEndGame.Loser;
                    //Context.isStartEndGame = true;
                    Context.waitNextState = WaitNextState.NextStartEndGame;
                }
                else
                {
                    //Context.isStartSetup = true;
                    Context.waitNextState = WaitNextState.NextStartSetup;
                }

                HandManager.Instance.StartHandCutOff(GameManager.Instance.CheckHealthPlayer());
            }
            else
            {
                SignatureDrawer.Instance.ResetDraw();
                if (GameManager.Instance.CheckPaper())
                {
                    Context.resultEndGame = ResultEndGame.Winner;
                    Context.isStartEndGame = true;
                }
                else
                {
                    Context.isStartSetup = true;
                }
            }
        }
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        Context.isStartEndGame = false;
        Context.isStartSetup = false;
    }

    public override GameStateMachine.EState GetNextState()
    {
        if(Context.isStartEndGame)
        {
            return GameStateMachine.EState.GameEnd;
        }
        else if (Context.isStartSetup)
        {
            return GameStateMachine.EState.Setup;
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
