using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseState : GameState
{
    public GamePauseState(GameStateContext context, GameStateMachine.EState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {
        Time.timeScale = 0;
        UIManager.Instance.SetActiveGamePausePanel(true);
    }
    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) GameStateMachine.Instance.ContinueGame();
    }

    public override void ExitState()
    {
        Context.isContinueGame = false;
        Time.timeScale = 1;
        UIManager.Instance.SetActiveGamePausePanel(false);
    }

    public override GameStateMachine.EState GetNextState()
    {
        if(Context.isContinueGame)
        {
            return GameStateMachine.EState.GamePlay;
        }
        else if (Context.isRestartGame)
        {
            Time.timeScale = 1;
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
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
