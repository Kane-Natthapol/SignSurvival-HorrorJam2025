using CustomInspector;
using System.Collections.Generic;
using UnityEngine;

public class SignatureEvaluator : Singleton<SignatureEvaluator>
{
    [HorizontalLine("SIGNATURE EVALUATOR DATA", 1, FixedColor.CloudWhite)]
    [Range(0f, 1f)] public float threshold = 0.8f;

    [Header("Reference Line Settings")]
    public LineRenderer referenceLine;
    public Color referenceColor = new Color(1f, 1f, 1f, 0.2f);

    private SignatureData currentData;

    public void SetUpEvaluator(float eva)
    {
        threshold = eva;
    }

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

        var localRef = new List<Vector3>(currentData.referencePoints);
        float similarity = ComparePaths(drawnPoints, localRef);
        Debug.Log($"Similarity: {similarity:F2}");

        string resultText = string.Empty;
        bool isPass = false;

        if (similarity >= threshold)
        {
            Debug.Log("<color=green>PASS</color>");
            resultText = "<color=green>PASS</color>";
            isPass = true;
        }
        else
        {
            Debug.Log("<color=red>FAIL</color>");
            resultText = "<color=red>FAIL</color>";
            isPass = false;
        }

        UIManager.Instance.SetTextResult(resultText);
        GameStateMachine.Instance.StartResult(isPass);
    }

    void DrawReferenceLine(SignatureData data)
    {
        if (referenceLine == null) return;

        if (data.referencePoints.Count == 0)
        {
            referenceLine.positionCount = 0;
            return;
        }

        referenceLine.useWorldSpace = false;
        referenceLine.positionCount = data.referencePoints.Count;
        referenceLine.SetPositions(data.referencePoints.ToArray());
        referenceLine.startColor = referenceColor;
        referenceLine.endColor = referenceColor;
        referenceLine.enabled = true;
    }

    float ComparePaths(List<Vector3> drawn, List<Vector3> reference)
    {
        if (drawn.Count == 0 || reference.Count == 0)
            return 0f;

        int sampleCount = 100;

        var d = Resample(drawn, sampleCount);
        var r = Resample(reference, sampleCount);

        NormalizePath(d);
        NormalizePath(r);

        float bestSimilarity = 0f;
        for (int offset = 0; offset < sampleCount; offset++)
        {
            float totalDist = 0f;
            for (int i = 0; i < sampleCount; i++)
            {
                int j = (i + offset) % sampleCount;
                totalDist += Vector3.Distance(d[i], r[j]);
            }

            float avgDist = totalDist / sampleCount;
            float similarity = Mathf.Clamp01(1 - avgDist / 2f);

            if (similarity > bestSimilarity)
                bestSimilarity = similarity;
        }

        List<Vector3> reversed = new List<Vector3>(d);
        reversed.Reverse();

        for (int offset = 0; offset < sampleCount; offset++)
        {
            float totalDist = 0f;
            for (int i = 0; i < sampleCount; i++)
            {
                int j = (i + offset) % sampleCount;
                totalDist += Vector3.Distance(reversed[i], r[j]);
            }

            float avgDist = totalDist / sampleCount;
            float similarity = Mathf.Clamp01(1 - avgDist / 2f);

            if (similarity > bestSimilarity)
                bestSimilarity = similarity;
        }

        return bestSimilarity;
    }

    void NormalizePath(List<Vector3> path)
    {
        Vector3 center = Vector3.zero;
        foreach (var p in path)
            center += p;
        center /= path.Count;

        for (int i = 0; i < path.Count; i++)
            path[i] -= center;

        float maxDist = 0f;
        foreach (var p in path)
            maxDist = Mathf.Max(maxDist, p.magnitude);

        if (maxDist > 0)
        {
            for (int i = 0; i < path.Count; i++)
                path[i] /= maxDist;
        }
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
