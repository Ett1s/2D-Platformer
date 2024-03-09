using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeVoice : MonoBehaviour
{
    private AudioSource audioSrc;
    public static float voicVolume = 1f;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }


    void Update()
    {
        audioSrc.volume = voicVolume;
    }

    public void SetVolume(float vol)
    {
        voicVolume = vol;
        PlayerPrefs.SetFloat("voicevolume", voicVolume);
    }

    public void SaveVolume(float newVolume)
    {
        voicVolume = newVolume;
        PlayerPrefs.SetFloat("voicevolume", voicVolume);
        audioSrc.volume = voicVolume;
    }
}