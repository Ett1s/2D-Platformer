using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource audioSrc;
    public static float musicVolume = 1f;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void SaveVolume(float newVolume)
    {
        musicVolume = newVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
        audioSrc.volume = musicVolume;
    }
}
