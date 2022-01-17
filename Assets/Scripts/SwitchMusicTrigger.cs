using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SwitchMusicTrigger
{
    public AudioClip clip;
    public string misc;
    public float volume;
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}