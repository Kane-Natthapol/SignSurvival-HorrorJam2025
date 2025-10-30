using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : Singleton<LightManager>
{
    [SerializeField] Light2D spotLightSignature;
    [SerializeField] Animator spotLightSigAnim;
    [SerializeField] Light2D spotLightHand;
    [SerializeField] Animator spotLightHandAnim;

    public void SetActiveLight(LightType type, bool isBool)
    {
        switch(type)
        {
            case LightType.Signature:
                SetActiveLightType(spotLightSigAnim, isBool);
                SetActiveLightType(spotLightHandAnim, !isBool);
                break;

            case LightType.Hand:
                SetActiveLightType(spotLightSigAnim, !isBool);
                SetActiveLightType(spotLightHandAnim, isBool);
                break;
        }
    }

    public void SetActiveLightType(Animator anim, bool isOn)
    {
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        if (isOn) 
        {
            if (!currentState.IsTag("In"))
            {
                anim.Play("Light_In", 0, 0);
            }
        }
        else
        {
            if (!currentState.IsTag("Out"))
            {
                anim.Play("Light_Out", 0, 0);
            }
        }
    }

    public void SetLightOutSingnature() => spotLightSigAnim.Play("Light_Over", 0, 0);
    public void SetLightIdleSingnature(int random)
    {
        switch(random)
        {
            case 0:
                spotLightSigAnim.Play("Light_Idle", 0, 0);
                break;
            default:
                spotLightSigAnim.Play("Light_Idle2", 0, 0);
                break;
        }
    }
}
