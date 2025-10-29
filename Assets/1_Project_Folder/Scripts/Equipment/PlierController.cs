using System;
using UnityEngine;

public class PlierController : MonoBehaviour
{
    [SerializeField] Animator plierAnimator;
    public Action OnCutoff;
    public Action OnCutoffFinished;

    public void PlierCutOff()
    {
        OnCutoff?.Invoke();
    }

    public void PlierCutOffFinished()
    {
        OnCutoffFinished?.Invoke();
    }

    public void StartCutOff()
    {
        plierAnimator.Play("Plier_Cutoff", 0, 0);
    }

    public void ApplyNail(GameObject nail)
    {
        nail.transform.parent = transform;
    }
}
