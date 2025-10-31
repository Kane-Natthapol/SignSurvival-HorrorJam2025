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
    [SerializeField] GameObject gameWinPanel;
    [SerializeField] GameObject gameLosePanel;
    [SerializeField] Button restartButton;
    [SerializeField] Button restartButtonTwo;

    [HorizontalLine("GAME PAUSE", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject gamePausePanel;
    [SerializeField] Button gameContinueButton;
    [SerializeField] Button restartGamePauseButtonTwo;

    [HorizontalLine("GAME PLAY", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject gamePlayPanel;
    [SerializeField] TextMeshProUGUI timeCoolDownText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI pageCountText;
    [SerializeField] TextMeshProUGUI resultText;

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
        restartButtonTwo.onClick.AddListener(ClickReStartGame);
    }

    #region MAIN MENU PANEL
    public void SetActiveMainMenuPanel(bool isBool) => mainMenuPanel.SetActive(isBool);

    void ClickStartGame()
    {
        SoundManager.Instance.PlayOneShot(GlobalConstraints.STRING_SOUND_SFX_CLICK);
        SoundManager.Instance.PlayOneShot(GlobalConstraints.STRING_SOUND_SFX_START);
        SoundManager.Instance.SetPlayBGMmainMenu(false);

        SetActiveMainMenuPanel(false);
        CameraManager.Instance.CameraSetUp();
    }

    void ClickQuitGame()
    {
        SoundManager.Instance.PlayOneShot(GlobalConstraints.STRING_SOUND_SFX_CLICK);
        Application.Quit();
    }
    #endregion

    #region GAME END PANEL
    public void SetActiveGameEndPanel(bool isBool) => gameEndPanel.SetActive(isBool);
    public void SetActiveGameWinPanel(bool isBool) => gameWinPanel.SetActive(isBool);
    public void SetActiveGameLosePanel(bool isBool) => gameLosePanel.SetActive(isBool);

    void ClickReStartGame()
    {
        SoundManager.Instance.PlayOneShot(GlobalConstraints.STRING_SOUND_SFX_CLICK);
        UIFadeManager.Instance.FadeIn();
    }
    //public void SetTextResultEndGame(string text) => resultEndGameText.text = $"{text}";
    #endregion

    #region GAME PLAY
    public void SetActiveGamePlayPanel(bool isBool) => gamePlayPanel.SetActive(isBool);

    public void SetTextTimeCoolDown(string time) => timeCoolDownText.text = $"{time.ToString()}";
    public void SetTextHealth(string hp) => healthText.text = $"HP : {hp}";
    public void SetTextPageCount(string currentPage) => pageCountText.text = $"{currentPage}";
    public void SetTextResult(string text) => resultText.text = $"{text}";
    #endregion

    void NothingMethoid() { }
}