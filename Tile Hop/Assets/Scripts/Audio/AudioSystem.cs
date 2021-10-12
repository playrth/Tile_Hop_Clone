using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem
{
    private static AudioSystem instance;
    private static SoundData[] sound;
    public static AudioSystem Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioSystem();
            }
            return instance;
        }
    }
    public SoundData[] Sound
    {
        set
        {
            sound = value;
        }
    }
    public void PlaySound(string x, GameObject sg)
    {
        if (sound != null && sound.Length == 0)
        {
            Debug.Log("Sound Manager Empty");
            return;
        }
        foreach (SoundData s in sound)
        {
            if (s.name == x)
            {
                AudioSource a = sg.GetComponent<AudioSource>();
                if (a != null)
                {
                    a.clip = s.clip;
                    a.loop = s.loop;
                    a.volume = s.Volume;
                    if (s.name == "Bounce")
                        a.volume = Random.Range(0.1f, 0.5f);
                    a.Play();
                }
            }
        }
    }
    public void StopSound(GameObject sg)
    {
        AudioSource a = sg.GetComponent<AudioSource>();
        if (a != null)
        {
            a.Stop();
        }
    }
}
