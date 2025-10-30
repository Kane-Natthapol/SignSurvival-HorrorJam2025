using System.Collections.Generic;
using UnityEngine;

public class HandManager : Singleton<HandManager>
{
    [SerializeField] Animator handAnimator;
    [SerializeField] Animator handCuffAnimator;

    [SerializeField] List<FingerController> fingerControllers;
    [SerializeField] List<PlierController> plierControllers;
    [SerializeField] List<KnifeController> knifeControllers;
    [SerializeField] List<BloodFingerManager> bloodFingerManagers;

    [SerializeField] int currentIndex;

    private void Start()
    {
        foreach (var plier in plierControllers)
        {
            plier.OnCutoffFinished += GameStateMachine.Instance.ResultFinised;
        }

        foreach (var knife in knifeControllers)
        {
            knife.OnCutoffFinished += GameStateMachine.Instance.ResultFinised;
        }

        /*StartHandCutOff(9);
        StartHandCutOff(8);
        StartHandCutOff(7);
        StartHandCutOff(6);
        StartHandCutOff(5);
        StartHandCutOff(4);
        StartHandCutOff(3);
        StartHandCutOff(2);
        StartHandCutOff(1);
        StartHandCutOff(0);*/
    }

    public void StartHandCutOff(int index)
    {
        currentIndex = index;

        switch (currentIndex)
        {
            case 9:
                PlierNailStart(0);
                break;

            case 8:
                PlierNailStart(1);
                break;

            case 7:
                PlierNailStart(2);
                break;

            case 6:
                PlierNailStart(3);
                break;

            case 5:
                PlierNailStart(4);
                break;

            case 4:
                knifeFingerStart(0);
                break;

            case 3:
                knifeFingerStart(1);
                break;

            case 2:
                knifeFingerStart(2);
                break;

            case 1:
                knifeFingerStart(3);
                break;

            case 0:
                StartHandCutOffAll(0);
                break;
        }
    }

    void PlierNailStart(int index)
    {
        plierControllers[index].OnCutoff += ()=>plierControllers[index].ApplyNail(fingerControllers[index].GetNail());
        plierControllers[index].StartCutOff();
        Scare();
    }

    void knifeFingerStart(int index)
    {
        knifeControllers[index].OnCutoff += fingerControllers[index].CutOff;
        knifeControllers[index].OnCutoff += CameraManager.Instance.CameraShake;
        knifeControllers[index].OnCutoff += GameManager.Instance.SetActivateTableBlood;
        knifeControllers[index].OnCutoff += ()=> bloodFingerManagers[index].PlayBloodForDuration(GlobalConstraints.FLOAT_BLOOD_FINGER_DURATION);
        knifeControllers[index].StartCutOff();
        Scare();
    }

    public void StartHandCutOffAll(int index)
    {
        currentIndex = index;

        if(currentIndex > 1)
        {
            knifeControllers[3].OnCutoff += fingerControllers[0].CutOff;
            knifeControllers[3].OnCutoff += () => bloodFingerManagers[0].PlayBloodForDuration(GlobalConstraints.FLOAT_BLOOD_FINGER_DURATION);

            knifeControllers[3].OnCutoff += fingerControllers[1].CutOff;
            knifeControllers[3].OnCutoff += () => bloodFingerManagers[1].PlayBloodForDuration(GlobalConstraints.FLOAT_BLOOD_FINGER_DURATION);

            knifeControllers[3].OnCutoff += fingerControllers[2].CutOff;
            knifeControllers[3].OnCutoff += () => bloodFingerManagers[2].PlayBloodForDuration(GlobalConstraints.FLOAT_BLOOD_FINGER_DURATION);

            knifeControllers[3].OnCutoff += fingerControllers[3].CutOff;
            knifeControllers[3].OnCutoff += () => bloodFingerManagers[3].PlayBloodForDuration(GlobalConstraints.FLOAT_BLOOD_FINGER_DURATION);

            knifeControllers[3].StartCutOff();
        }

        knifeControllers[4].OnCutoff += fingerControllers[4].CutOff;
        knifeControllers[4].OnCutoff += () => bloodFingerManagers[4].PlayBloodForDuration(GlobalConstraints.FLOAT_BLOOD_FINGER_DURATION);
        knifeControllers[4].OnCutoff += CameraManager.Instance.CameraShake;
        knifeControllers[4].OnCutoff += GameManager.Instance.SetActivateTableBlood;
        knifeControllers[4].StartCutOff();
        Scare();
    }

    void Scare()
    {
        handAnimator.Play("Hand_Scare", 0, 0);
        handCuffAnimator.Play("Handcuff_Shake", 0, 0);
        foreach (var finger in fingerControllers)
        {
            finger.Scare();
        }
    }

    public void Idle()
    {
        handAnimator.Play("Hand_Idle", 0, 0);
        handCuffAnimator.Play("Handcuff_Idle", 0, 0);
        foreach (var finger in fingerControllers)
        {
            finger.Idle();
        }
    }

}
