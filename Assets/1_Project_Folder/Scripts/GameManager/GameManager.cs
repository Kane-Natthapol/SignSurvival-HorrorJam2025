using CustomInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [HorizontalLine("PLAYER DATA", 1, FixedColor.CloudWhite)]
    [SerializeField] public int maxHealth = 0;
    [SerializeField] public int currentHealth = 0;

    [HorizontalLine("GAME DATA", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isRunTime = false;
    [SerializeField] public float maxTime = 0f;
    [SerializeField] public float currentTime = 0f;

    [HorizontalLine("FaceBlood", 1, FixedColor.CloudWhite)]
    [SerializeField] float randomFaceBlood;
    [SerializeField] GameObject faceBloodGameObject;

    [HorizontalLine("TableBlood", 1, FixedColor.CloudWhite)]
    [SerializeField] GameObject TableBloodOne;
    [SerializeField] GameObject TableBloodTwo;

    [HorizontalLine("PaperBlood", 1, FixedColor.CloudWhite)]
    [SerializeField] float randomPaperBlood;

    [HorizontalLine("Paper DATA", 1, FixedColor.CloudWhite)]
    [SerializeField] public int maxPaper = 0;
    [SerializeField] public int currentPaper = 0;

    [HorizontalLine("SIGNATURE EVALUATOR DATA", 1, FixedColor.CloudWhite)]
    [Range(0f, 1f)] public float evaluatorThreshold = 0.8f;

    [HorizontalLine("LightOut", 1, FixedColor.CloudWhite)]
    [SerializeField] int startPaperRandom;
    [SerializeField] float randomLightOut;

    [HorizontalLine("Paper level DATA", 1, FixedColor.CloudWhite)]
    [SerializeField] public int mediumLevelSignature = 0;
    [SerializeField] public int hardLevelSignature = 0;

    private void Update()
    {
        if(isRunTime && maxTime > 0)
        {
            if(currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = 0f;
            }
        }

        AddGameTime();
    }

    public void SetUpGame()
    {
        currentHealth = maxHealth;
        currentTime = maxTime;
        currentPaper = 0;

        SetIsRunTime(false);

        SignatureEvaluator.Instance.SetUpEvaluator(evaluatorThreshold);
        UIManager.Instance.SetTextTimeCoolDown(currentTime.ToString("f0"));
        UIManager.Instance.SetTextHealth(currentHealth.ToString("f0"));
        UIManager.Instance.SetTextPageCount(((maxPaper - currentPaper) + 1).ToString("f0"));
    }

    public void AddHealthPlayer(int num)
    {
        currentHealth -= num;
        UIManager.Instance.SetTextHealth(currentHealth.ToString("f1"));
    }
    public int CheckHealthPlayer()=> currentHealth;

    public void SetIsRunTime(bool isBool) => isRunTime = isBool;
    public float CheckTime()=> currentTime;
    public void AddGameTime()
    {
        UIManager.Instance.SetTextTimeCoolDown(currentTime.ToString("f1"));
    }

    public bool CheckPaper()=> currentPaper == maxPaper;
    public void AddPaper(int num)
    {
        currentPaper += num;
        UIManager.Instance.SetTextPageCount(((maxPaper - currentPaper) + 1).ToString("f0"));
    }

    public void SetRandomBlood()
    {
        float randomFaceBlood = Random.Range(0, 100);
        if(randomFaceBlood >= randomFaceBlood)
        {
            SetActiveFaceBlood(false);
            SetActiveFaceBlood(true);
        }

        float randomPaperBlood = Random.Range(0, 100);
        if (randomPaperBlood >= randomPaperBlood)
        {
            PaperManager.Instance.SetActiveBloodPaper(true);
        }
    }
    public void SetActiveFaceBlood(bool isBool) => faceBloodGameObject.SetActive(isBool);


    public bool CheckCanRandomLightOut() => currentPaper >= startPaperRandom;
    public void SetRandomRightOut()
    {
        float random = Random.Range(0, 100);

        if (randomLightOut >= random)
        {
            LightManager.Instance.SetLightOutSingnature();
        }
    }

    public void ReStartGame()
    {
        Time.timeScale = 1;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void SetActivateTableBlood()
    {
        if(TableBloodOne.activeSelf == false)
        {
            TableBloodOne.SetActive(true);
        }
        else if(TableBloodTwo.activeSelf == false)
        {
            TableBloodTwo.SetActive(true);
        }
    }

    public bool CheckHardLevel() => currentPaper >= hardLevelSignature;
    public bool CheckMediumLevel() => currentPaper >= mediumLevelSignature;
}
