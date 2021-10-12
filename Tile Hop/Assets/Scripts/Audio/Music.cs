using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Music", order = 50)]
public class Music : ScriptableObject
{
    [SerializeField]
    private SoundData[] sound;
    public SoundData[] Sound
    {
        get
        {
            return sound;
        }
    }
}
