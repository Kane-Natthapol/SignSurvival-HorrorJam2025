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
    [SerializeField] Button quitGameButtonTwo;
    [SerializeField] Button restartGameButtonTwo;

    [HorizontalLine("GAME END", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] Button restartButton;

    [HorizontalLine("GAME PAUSE", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject gamePausePanel;
    [SerializeField] Button gameContinueButton;

    [HorizontalLine("GAME PLAY", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject gamePlayPanel;
    [SerializeField] TextMeshProUGUI gameWaveText;
    [SerializeField] TextMeshProUGUI gameEnemySpawnText;
    [SerializeField] TextMeshProUGUI gameEnemyDieText;
    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI wildFireCooldownText;

    [HorizontalLine("UPGRADE", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject upgradePanel;
    [SerializeField] Button selectUpgradeButton;
    [SerializeField] Button rerollUpgradeButton;
    [SerializeField] TextMeshProUGUI rerollUpgradeText;
    [SerializeField] TextMeshProUGUI logUpgradeText;
    [SerializeField] TextMeshProUGUI logUpgradeTextEnd;

    private void Start()
    {
        SubscriptionUI();
    }

    void SubscriptionUI()
    {
        //MAIN MENU
        startGameButton.onClick.AddListener(ClickStartGame);
        quitGameButton.onClick.AddListener(ClickQuitGame);
        quitGameButtonTwo.onClick.AddListener(ClickQuitGame);

        //GAME END
        restartButton.onClick.AddListener(ClickReStartGame);
        restartGameButtonTwo.onClick.AddListener(ClickReStartGame);

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

    #region GAME PLAY PANEL
    public void SetActiveGamePlayPanel(bool isBool) => gamePlayPanel.SetActive(isBool);
    public void SetActiveWildFireCooldownText(bool isBool) => wildFireCooldownText.gameObject.SetActive(isBool);
    public void SetTextGameWave(string text) => gameWaveText.text = text;
    public void SetTextEnemySpawn(string text) => gameEnemySpawnText.text = text;
    public void SetTextEnemyDie(string text) => gameEnemyDieText.text = text;
    public void SetTextPlayerHealth(string text) => playerHealthText.text = text;
    public void SetTextWildfireCooldown(string text) => wildFireCooldownText.text = text;
    #endregion

    #region UPGRADE PANEL
    public void SetActiveUpgradePanel(bool isBool) => upgradePanel.SetActive(isBool);
    public void SetInteractableSelectUpgradeButton(bool isBool) => selectUpgradeButton.interactable = isBool;
    public void SetInteractableRerollUpgradeButton(bool isBool) => rerollUpgradeButton.interactable = isBool;
    public void SetTextRerollUpgradeButton(string text) => rerollUpgradeText.text = text;
    #endregion

    public void SetTextLogUpgradeText(string text) => logUpgradeText.text = text += logUpgradeText.text;
    public void SetTextLogUpgradeTextEnd() => logUpgradeTextEnd.text = logUpgradeText.text;

    void ClickQuitGame()  =>  Application.Quit();

    void NothingMethoid() { }
}