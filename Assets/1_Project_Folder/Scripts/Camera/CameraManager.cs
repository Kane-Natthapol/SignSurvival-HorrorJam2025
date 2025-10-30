using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] Animator cameraAnimator;

    public void CameraShake()
    {
        cameraAnimator.Play("Camera_Shake", 0, 0);
    }

    public void CameraMainmenu()
    {
        cameraAnimator.Play("Camera_Mainmenu", 0, 0);
    }

    public void CameraSetUp()
    {
        cameraAnimator.Play("Camera_SetUp", 0, 0);
    }

    public void OmSetUpFinished()
    {
        GameStateMachine.Instance.StartGame();
    }
}
