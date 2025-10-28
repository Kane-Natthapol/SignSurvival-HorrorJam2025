using UnityEngine;

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
    public bool StartGame() => context.isStartGame = true;

    //GAME PLAY
    public bool PauseGame() => context.isPauseGame = true;
    public bool WaveEndGame() => context.isWaveEnd = true;
    public bool EndGame() => context.isGameEnd = true;

    //GAME PAUSE
    public bool ContinueGame() => context.isContinueGame = true;

    //GAME UPGRADE
    public bool SelectedUpgrade() => context.isSelectedUppgrade = true;

    //Game End
    public bool RestartGame() => context.isRestartGame = true;

    private void InitializeStates()
    {
        States.Add(EState.Mainmenu, new GameMainmenuState(context, EState.Setup));
        States.Add(EState.Setup, new GameSetupState(context, EState.Setup));
        States.Add(EState.DrawTime, new GameDrawTimeState(context, EState.DrawTime));
        States.Add(EState.ShowResult, new GameShowResultState(context, EState.ShowResult));
        States.Add(EState.GameEnd, new GameEndState(context, EState.GameEnd));
        
        CurrentState = States[EState.Setup];
    }
}