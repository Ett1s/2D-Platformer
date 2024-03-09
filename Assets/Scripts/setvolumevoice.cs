using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setvolumevoice : MonoBehaviour
{
    private AudioSource audioSrc;
    public static float voicevolume;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        LoadVolume();
    }

    void Update()
    {

    }

    public void LoadVolume()
    {
        voicevolume = PlayerPrefs.GetFloat("voicevolume");
        audioSrc.volume = voicevolume;
    }
}