using UnityEngine;

public class GameMainmenuState : GameState
{
    public GameMainmenuState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        SoundManager.Instance.SetPlayBGMmainMenu(true);
        CameraManager.Instance.CameraMainmenu();
        UIManager.Instance.SetActiveMainMenuPanel(true);
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        UIManager.Instance.SetActiveMainMenuPanel(false);
        GameManager.Instance.SetUpGame();
        Context.isPass = true;

        Context.isStartGame = false;
    }

    public override GameStateMachine.EState GetNextState()
    {
        if(Context.isStartGame)
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
