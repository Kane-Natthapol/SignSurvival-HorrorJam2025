using UnityEngine;

public class GameDrawTimeState : GameState
{
    public GameDrawTimeState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        SoundManager.Instance.SetPlayBGMCountdown(true);
        SoundManager.Instance.SetPlayBGMCurOff(false);

        GameManager.Instance.SetIsRunTime(true);
        SignatureDrawer.Instance.CanDraw(true);

        if(GameManager.Instance.CheckCanRandomLightOut())
        {
            GameManager.Instance.SetRandomRightOut();
        }
        else
        {
            int Random = UnityEngine.Random.Range(0, 2);
            LightManager.Instance.SetLightIdleSingnature(Random);
        }
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
        SignatureDrawer.Instance.CanDraw(false);
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
