using CustomInspector;
using UnityEngine;

public class FingerController : MonoBehaviour
{
    [SerializeField] Animator fignerAnim;
    [SerializeField] GameObject nailObj;

    [HorizontalLine("Cut Off" , 1, FixedColor.CloudWhite)]
    [SerializeField] Rigidbody2D fignerRb;
    [SerializeField] float launchForce = 500f;
    [SerializeField] float rotationTorque = 50f;
    [SerializeField] Vector2 launchDirection = Vector2.right;
    [SerializeField] bool isCut = false;

    public GameObject GetNail()=> nailObj;
    public void CutOff()
    {
        if (isCut) return;
        isCut = true;

        fignerAnim.Play("Finger_Idle", 0, 0);
        fignerAnim.enabled = false;
        Launch();

        GameManager.Instance.SetRandomBlood();
    }

    public void Launch()
    {
        if (fignerRb == null) return;
        Vector2 finalForce = launchDirection.normalized * launchForce;
        fignerRb.AddTorque(rotationTorque, ForceMode2D.Impulse);
        fignerRb.AddForce(finalForce, ForceMode2D.Impulse);
    }

    public void Idle()
    {
        fignerAnim.Play("Finger_Idle", 0, 0);
    }

    public void Scare()
    {
        if (isCut) return;
        fignerAnim.Play("Finger_Scare", 0, 0);
    }
}