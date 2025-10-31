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
            SoundManager.Instance.SetPlayBGMCountdown(false);
            SoundManager.Instance.SetPlayBGMCurOff(true);

            LightManager.Instance.SetActiveLight(LightType.Hand, true);

            HandManager.Instance.StartHandCutOffAll(GameManager.Instance.CheckHealthPlayer());
            //Context.isStartEndGame = true;
            Context.waitNextState = WaitNextState.NextStartEndGame;
        }
        else
        {
            if (!Context.isPass)
            {
                GameManager.Instance.SetIsRunTime(false);
                SoundManager.Instance.SetPlayBGMCountdown(false);
                SoundManager.Instance.SetPlayBGMCurOff(true);

                LightManager.Instance.SetActiveLight(LightType.Hand, true);

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
                if (GameManager.Instance.CheckPaper())
                {
                    GameManager.Instance.SetIsRunTime(false);
                    Context.resultEndGame = ResultEndGame.Winner;
                    //Context.isStartEndGame = true;
                    Context.waitNextState = WaitNextState.NextStartEndGame;
                }
                else
                {
                    //Context.isStartSetup = true;
                    Context.waitNextState = WaitNextState.NextStartSetup;
                }

                SoundManager.Instance.PlayOneShot(GlobalConstraints.STRING_SOUND_SFX_PAGE_OUT);
                PaperManager.Instance.PaperOut();
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
