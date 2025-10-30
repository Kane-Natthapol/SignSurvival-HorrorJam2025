using System.Collections;
using UnityEngine;

public class BloodFingerManager : MonoBehaviour
{
    [SerializeField] ParticleSystem bloodParticle;
    bool isFinished;

    public void PlayBloodForDuration(float duration)
    {
        if (isFinished) return;
        isFinished = true;
        StartCoroutine(PlayAndStopAfterDelay(duration));
    }

    private IEnumerator PlayAndStopAfterDelay(float delay)
    {
        if (bloodParticle == null)
        {
            Debug.LogWarning("No Particle System");
            yield break;
        }

        bloodParticle.Play();

        yield return new WaitForSeconds(delay);

        bloodParticle.Stop();
    }
}
