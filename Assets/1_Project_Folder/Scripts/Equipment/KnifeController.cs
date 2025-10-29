using System;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [SerializeField] Animator knifeAnimator;
    public Action OnCutoff;
    public Action OnCutoffFinished;

    public void KnifeCutOff()
    {
        OnCutoff?.Invoke();
    }

    public void KnifeCutOffFinished()
    {
        OnCutoffFinished?.Invoke();
    }

    public void StartCutOff()
    {
        knifeAnimator.Play("Knife_Cutoff", 0, 0);
    }
}
