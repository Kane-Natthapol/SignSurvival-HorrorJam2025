using CustomInspector;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HorizontalLine("PLAYER DATA", 1, FixedColor.CloudWhite)]
    [SerializeField] public int maxHealth = 0;
    [SerializeField] public int currentHealth = 0;

    [HorizontalLine("GAME DATA", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isRunTime = false;
    [SerializeField] public float maxTime = 0f;
    [SerializeField] public float currentTime = 0f;

    [HorizontalLine("SIGNATURE DATA", 1, FixedColor.CloudWhite)]
    [SerializeField] public int maxPaper = 0;
    [SerializeField] public int currentPaper = 0;

    [HorizontalLine("SIGNATURE EVALUATOR DATA", 1, FixedColor.CloudWhite)]
    [Range(0f, 1f)] public float evaluatorThreshold = 0.8f;

    private void Update()
    {
        if(isRunTime && maxTime > 0)
        {
            if(currentTime >= 1)
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
        UIManager.Instance.SetTextPageCount(currentPaper.ToString("f0"), maxPaper.ToString("f0"));
    }

    public void AddHealthPlayer(int num)
    {
        currentHealth -= num;
        UIManager.Instance.SetTextHealth(currentHealth.ToString("f0"));
    }
    public int CheckHealthPlayer()=> currentHealth;

    public void SetIsRunTime(bool isBool) => isRunTime = isBool;
    public float CheckTime()=> currentTime;
    public void AddGameTime()
    {
        UIManager.Instance.SetTextTimeCoolDown(currentTime.ToString("f0"));
    }

    public bool CheckPaper()=> currentPaper == maxPaper;
    public void AddPaper(int num)
    {
        currentPaper += num;
        UIManager.Instance.SetTextPageCount(currentPaper.ToString("f0"), maxPaper.ToString("f0"));
    }
}
