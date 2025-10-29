using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : StateManager<GameStateMachine.EState>
{
    public enum EState
    {
        Mainmenu,
        Setup,
        DrawTime,
        ShowResult,
        GameEnd
    }

    [SerializeField] GameStateContext context;
    public static GameStateMachine Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //_context = new GameStateContext(this);
        context.stateMachine = this;

        InitializeStates();
    }

    // MAIN MENU
    public void StartGame() => context.isStartGame = true;

    //GAME PLAY
    public void StartDraw() => context.isStartDraw = true;
    public void StartResult(bool isBool)
    {
        context.isPass = isBool;
        context.isStartResult = true;
    }

    public void StartSetup() => context.isStartSetup = true;
    public void StartEndGame() => context.isStartEndGame = true;

    //GAME END
    public void StartReStartGame() => context.isRestartGame = true;

    public void RestartGame()
    {
        Time.timeScale = 1;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void ResultFinised()
    {
        if(context.waitNextState == WaitNextState.NextStartEndGame)
        {
            context.isStartEndGame = true;
        }
        else if(context.waitNextState == WaitNextState.NextStartSetup)
        {
            context.isStartSetup = true;
        }
    }

    private void InitializeStates()
    {
        States.Add(EState.Mainmenu, new GameMainmenuState(context, EState.Mainmenu));
        States.Add(EState.Setup, new GameSetupState(context, EState.Setup));
        States.Add(EState.DrawTime, new GameDrawTimeState(context, EState.DrawTime));
        States.Add(EState.ShowResult, new GameShowResultState(context, EState.ShowResult));
        States.Add(EState.GameEnd, new GameEndState(context, EState.GameEnd));
        
        CurrentState = States[EState.Mainmenu];
    }
}