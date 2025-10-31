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

        SoundManager.Instance.SetPlayBGMCountdown(false);
        SoundManager.Instance.SetPlayBGMCurOff(false);

        if (Context.resultEndGame == ResultEndGame.Winner)
        {
            SoundManager.Instance.PlayOneShot(GlobalConstraints.STRING_SOUND_SFX_YEAH);
            UIManager.Instance.SetActiveGameWinPanel(true);
        }
        else if(Context.resultEndGame == ResultEndGame.Loser)
        {
            UIManager.Instance.SetActiveGameLosePanel(true);
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
