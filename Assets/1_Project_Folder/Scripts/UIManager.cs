using CustomInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [HorizontalLine("MAINMENU", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] Button startGameButton;
    [SerializeField] Button quitGameButton;


    [HorizontalLine("GAME END", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] Button restartButton;

    [HorizontalLine("GAME PAUSE", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject gamePausePanel;
    [SerializeField] Button gameContinueButton;
    [SerializeField] Button restartGamePauseButtonTwo;

    [HorizontalLine("GAME PLAY", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject gamePlayPanel;
    [SerializeField] TextMeshProUGUI timeCoolDownText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI pageCountText;

    //[HorizontalLine("SETTING", 1, FixedColor.CloudWhite)]

    private void Start()
    {
        SubscriptionUI();
    }

    void SubscriptionUI()
    {
        //MAIN MENU
        startGameButton.onClick.AddListener(ClickStartGame);
        quitGameButton.onClick.AddListener(ClickQuitGame);

        //GAME END
        restartButton.onClick.AddListener(ClickReStartGame);

        //GAME PAUSE
        gameContinueButton.onClick.AddListener(ClickContinue);
    }

    #region MAIN MENU PANEL
    public void SetActiveMainMenuPanel(bool isBool) => mainMenuPanel.SetActive(isBool);

    void ClickStartGame() => GameStateMachine.Instance.StartGame();
    #endregion

    #region GAME END PANEL
    public void SetActiveGameEndPanel(bool isBool) => gameEndPanel.SetActive(isBool);

    void ClickReStartGame() => GameStateMachine.Instance.RestartGame();
    #endregion

    #region GAME PAUSE PANEL
    public void SetActiveGamePausePanel(bool isBool) => gamePausePanel.SetActive(isBool);

    void ClickContinue() => NothingMethoid();
    #endregion

    void ClickQuitGame()  =>  Application.Quit();

    void NothingMethoid() { }
}