using UnityEngine;

public class SignatureTaskManager : MonoBehaviour
{
    public SignatureData[] allSignatures;
    public int currentIndex = 0;

    void Start()
    {
        LoadSignature(currentIndex);
    }

    public void LoadSignature(int index)
    {
        if (index < 0 || index >= allSignatures.Length) return;
        SignatureEvaluator.Instance.SetSignature(allSignatures[index]);
        Debug.Log($"Loaded Signature: {allSignatures[index].signatureName}");
    }

    public void NextSignature()
    {
        currentIndex = (currentIndex + 1) % allSignatures.Length;
        LoadSignature(currentIndex);
    }
}