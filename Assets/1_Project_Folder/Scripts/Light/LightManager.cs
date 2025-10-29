using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : Singleton<LightManager>
{
    [SerializeField] Light2D spotLightSignature;
    [SerializeField] Animator spotLightSigAnim;
    [SerializeField] Light2D spotLightHand;
    [SerializeField] Animator spotLightHandAnim;

    public void SetActiveLightSignature(bool isBool)
    {
        spotLightSignature.gameObject.SetActive(isBool);
    }

    public void SetActiveLightHand(bool isBool)
    {
        spotLightHand.gameObject.SetActive(isBool);
    }
}
