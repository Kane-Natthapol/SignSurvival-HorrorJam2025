using System.Collections.Generic;
using UnityEngine;

public class SignatureTaskManager : Singleton<SignatureTaskManager>
{
    public List<SignatureData> allSignatures;
    public int currentIndex = 0;

    public void LoadSignature()
    {
        SignatureDrawer.Instance.ResetDraw();
        currentIndex = Random.Range(0, allSignatures.Count);

        if (currentIndex < 0 || currentIndex >= allSignatures.Count) return;
        SignatureEvaluator.Instance.SetSignature(allSignatures[currentIndex]);
        Debug.Log($"Loaded Signature: {allSignatures[currentIndex].signatureName}");

        allSignatures.Remove(allSignatures[currentIndex]);
    }

/*    public void NextSignature()
    {
        currentIndex = (currentIndex + 1) % allSignatures.Length;
        LoadSignature(currentIndex);
    }*/
}