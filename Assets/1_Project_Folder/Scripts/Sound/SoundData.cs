using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Objects/SoundData")]
public class SoundData : ScriptableObject
{
    public List<SoundInfo> Sounds;

}
[Serializable]
public class SoundInfo
{
    public string SoundName;
    public AudioClip SoundClip;
}
