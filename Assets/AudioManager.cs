using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider soundSlider;
    public static float vol = 0.3f;
    public void zmenaHlasitosti()
    {
        vol = soundSlider.value * 0.3f;
        foreach(AudioSource aud in FindObjectsOfType<AudioSource>())
        {
            if (aud.GetComponentInParent<Enemy>())
            {
                aud.volume = vol*0.3f;
            } else
                aud.volume = vol;
        }
    }
    public static void StopSound(bool pauza)
    {
        foreach (AudioSource aud in FindObjectsOfType<AudioSource>())
        {
            aud.enabled = pauza;
        }
    }
}
