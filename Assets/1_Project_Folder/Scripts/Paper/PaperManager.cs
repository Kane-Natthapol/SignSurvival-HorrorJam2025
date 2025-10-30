using System;
using UnityEngine;

public class PaperManager : Singleton<PaperManager>
{
    [SerializeField] Animator paperAnimator;

    [SerializeField] GameObject bloodGameObject;

    public Action OnPaperInStart;
    public Action OnPaperInFinished;
    public Action OnPaperOutFinished;

    private void Start()
    {
        OnPaperInStart += SignatureTaskManager.Instance.LoadSignature;
        OnPaperInStart += ()=>SetActiveBloodPaper(false);
        OnPaperInFinished += GameStateMachine.Instance.StartDraw;
        OnPaperOutFinished += GameStateMachine.Instance.ResultFinised;
    }

    public void PaperInStart() => OnPaperInStart?.Invoke();
    public void PaperInFinished() => OnPaperInFinished?.Invoke();
    public void PaperOutFinished() => OnPaperOutFinished?.Invoke();

    public void PaperStandby() => paperAnimator.Play("Paper_Standby_In", 0, 0);
    public void PaperIn() => paperAnimator.Play("Paper_In", 0, 0);
    public void PaperIdle() => paperAnimator.Play("Paper_Idle", 0, 0);
    public void PaperOut() => paperAnimator.Play("Paper_Out", 0, 0);

    public void SetActiveBloodPaper(bool isActive) => bloodGameObject.SetActive(isActive);
}
