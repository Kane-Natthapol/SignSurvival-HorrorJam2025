using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Signature/Signature Data")]
public class SignatureData : ScriptableObject
{
    public string signatureName;
    public List<Vector3> referencePoints = new List<Vector3>();
}
