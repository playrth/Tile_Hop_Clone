using UnityEngine;
[System.Serializable]
public class SoundData
{
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(0, 1)]
    public float Volume = 1;
}
