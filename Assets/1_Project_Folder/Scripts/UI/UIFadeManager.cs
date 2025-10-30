using UnityEngine;

public class UIFadeManager : Singleton<UIFadeManager>
{
    [SerializeField] Animator uiFadeAnimator;

    public void FadeIn()
    {
        uiFadeAnimator.Play("Fade_In", 0, 0);
    }

    public void FadeOut()
    {
        uiFadeAnimator.Play("Fade_Out", 0, 0);
    }

    public void FadeInComplete()
    {
        GameManager.Instance.ReStartGame();
    }
}
