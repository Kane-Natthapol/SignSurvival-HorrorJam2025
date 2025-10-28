#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class SignaturePathCopier : EditorWindow
{
    [MenuItem("Tools/Signature/Copy Line Path")]
    static void Init()
    {
        GetWindow<SignaturePathCopier>("Signature Path Copier");
    }

    LineRenderer line;
    SignatureData targetData;

    void OnGUI()
    {
        line = (LineRenderer)EditorGUILayout.ObjectField("LineRenderer", line, typeof(LineRenderer), true);
        targetData = (SignatureData)EditorGUILayout.ObjectField("Target Data", targetData, typeof(SignatureData), false);

        if (GUILayout.Button("Copy Path to Data"))
        {
            if (line && targetData)
            {
                targetData.referencePoints.Clear();
                Vector3[] pts = new Vector3[line.positionCount];
                line.GetPositions(pts);
                targetData.referencePoints.AddRange(pts);
                EditorUtility.SetDirty(targetData);
                Debug.Log("Path copied to SignatureData!");
            }
        }
    }
}
#endif
