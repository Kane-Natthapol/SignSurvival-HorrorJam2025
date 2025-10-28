using UnityEngine;

public class GameStateMachine : StateManager<GameStateMachine.EState>
{
    public enum EState
    {
        Setup,
        GamePlay,
        GameUpgrade,
        GamePause,
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
        States.Add(EState.Setup, new GameSetupState(context, EState.Setup));
        States.Add(EState.GamePlay, new GameplayState(context, EState.GamePlay));
        States.Add(EState.GameUpgrade, new GameUpgradeState(context, EState.GameUpgrade));
        States.Add(EState.GamePause, new GamePauseState(context, EState.GamePause));
        States.Add(EState.GameEnd, new GameEndState(context, EState.GameEnd));
        
        CurrentState = States[EState.Setup];
    }
}