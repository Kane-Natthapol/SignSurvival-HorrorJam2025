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
