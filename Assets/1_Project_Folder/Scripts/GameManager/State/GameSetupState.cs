using UnityEngine;

public class GameSetupState : GameState
{
    public GameSetupState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        UIManager.Instance.SetActiveGamePlayPanel(true);
        //UIManager.Instance.SetTextResult(string.Empty);
        if(GameManager.Instance.CheckHealthPlayer() > 4)HandManager.Instance.Idle();

        LightManager.Instance.SetActiveLight(LightType.Signature, true);

        if (Context.isPass)
        {
            GameManager.Instance.AddPaper(1);
            PaperManager.Instance.PaperIn();
        }
        else
        {
            Context.isStartDraw = true;
        }
    }

    public override void UpdateState()
    {
        //Context.isStartDraw = true;
    }

    public override void ExitState()
    {
        Context.isStartDraw = false;
    }

    public override GameStateMachine.EState GetNextState()
    {
        if(Context.isStartDraw)
        {
            return GameStateMachine.EState.DrawTime;
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
