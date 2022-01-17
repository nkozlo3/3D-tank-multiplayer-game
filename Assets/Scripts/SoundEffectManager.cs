using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class SoundEffectManager : MonoBehaviour
{
    // public AudioManager BGM;
    public SwitchMusicTrigger[] sounds;
    // Start is called before the first frame update
    void Awake()
    {

        foreach (SwitchMusicTrigger s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.pitch;
        }
    }

    public void Play(string misc)
    {
        SwitchMusicTrigger s = Array.Find(sounds, sound => sound.misc == misc);
        s.source.Play();
        if (misc != "Shoot" && misc != "BulletExplosion")
        {
            s.source.loop = true;
        }
    }
    public void Stop(string misc)
    {
        SwitchMusicTrigger s = Array.Find(sounds, sound => sound.misc == misc);
        s.source.Stop();
    }
}
