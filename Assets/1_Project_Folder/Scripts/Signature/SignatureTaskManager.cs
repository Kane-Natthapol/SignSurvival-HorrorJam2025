using System.Collections.Generic;
using UnityEngine;

public class SignatureTaskManager : Singleton<SignatureTaskManager>
{
    public List<SignatureData> easySignatures;
    public List<SignatureData> mediumSignatures;
    public List<SignatureData> hardSignatures;
    public int currentIndex = 0;

    public void LoadSignature()
    {
        SignatureDrawer.Instance.ResetDraw();

        if(GameManager.Instance.CheckHardLevel())
        {
            SelectSingnature(hardSignatures);
        }
        else if (GameManager.Instance.CheckMediumLevel())
        {
            SelectSingnature(mediumSignatures);
        }
        else
        {
            SelectSingnature(easySignatures);
        }
    }

    void SelectSingnature(List<SignatureData> listSing)
    {
        currentIndex = Random.Range(0, listSing.Count);

        if (currentIndex < 0 || currentIndex >= listSing.Count) return;
        SignatureEvaluator.Instance.SetSignature(listSing[currentIndex]);
        Debug.Log($"Loaded Signature: {listSing[currentIndex].signatureName}");

        listSing.Remove(listSing[currentIndex]);
    }

/*    public void NextSignature()
    {
        currentIndex = (currentIndex + 1) % allSignatures.Length;
        LoadSignature(currentIndex);
    }*/
}