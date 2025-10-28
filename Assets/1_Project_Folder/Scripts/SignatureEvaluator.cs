using UnityEngine;
using System.Collections.Generic;

public class SignatureEvaluator : MonoBehaviour
{
    public static SignatureEvaluator Instance;

    [Header("Evaluation Settings")]
    [Range(0f, 1f)] public float threshold = 0.8f;

    [Header("Reference Line Settings")]
    public LineRenderer referenceLine;
    public Color referenceColor = new Color(1f, 1f, 1f, 0.2f);

    private SignatureData currentData;

    void Awake()
    {
        Instance = this;
    }

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            SetSignature(currentData);
        }
    }*/

    public void SetSignature(SignatureData data)
    {
        currentData = data;
        DrawReferenceLine(data);
    }

    public void Evaluate(List<Vector3> drawnPoints)
    {
        if (currentData == null || currentData.referencePoints.Count == 0)
        {
            Debug.LogWarning("No signature reference loaded.");
            return;
        }

        float similarity = ComparePaths(drawnPoints, currentData.referencePoints);
        Debug.Log($"Similarity: {similarity:F2}");

        if (similarity >= threshold)
            Debug.Log("<color=green>PASS</color>");
        else
            Debug.Log("<color=red>FAIL</color>");
    }

    void DrawReferenceLine(SignatureData data)
    {
        if (referenceLine == null) return;

        if (data.referencePoints.Count == 0)
        {
            referenceLine.positionCount = 0;
            return;
        }

        referenceLine.positionCount = data.referencePoints.Count;
        referenceLine.SetPositions(data.referencePoints.ToArray());
        referenceLine.startColor = referenceColor;
        referenceLine.endColor = referenceColor;
        referenceLine.enabled = true;
    }

    float ComparePaths(List<Vector3> drawn, List<Vector3> reference)
    {
        if (drawn.Count == 0 || reference.Count == 0) return 0;

        int sampleCount = 100;
        var d = Resample(drawn, sampleCount);
        var r = Resample(reference, sampleCount);

        float totalDist = 0;
        for (int i = 0; i < sampleCount; i++)
            totalDist += Vector3.Distance(d[i], r[i]);

        float avgDist = totalDist / sampleCount;
        float maxDist = 2f;
        float similarity = Mathf.Clamp01(1 - (avgDist / maxDist));
        return similarity;
    }

    List<Vector3> Resample(List<Vector3> path, int count)
    {
        if (path.Count < 2) return path;

        float totalLength = 0f;
        for (int i = 1; i < path.Count; i++)
            totalLength += Vector3.Distance(path[i - 1], path[i]);

        List<Vector3> resampled = new List<Vector3> { path[0] };
        float interval = totalLength / (count - 1);
        float distSoFar = 0;

        for (int i = 1; i < path.Count; i++)
        {
            float segDist = Vector3.Distance(path[i - 1], path[i]);
            while (distSoFar + segDist >= interval)
            {
                float t = (interval - distSoFar) / segDist;
                Vector3 newPoint = Vector3.Lerp(path[i - 1], path[i], t);
                resampled.Add(newPoint);
                segDist -= (interval - distSoFar);
                distSoFar = 0;
            }
            distSoFar += segDist;
        }

        while (resampled.Count < count)
            resampled.Add(path[^1]);

        return resampled;
    }
}
