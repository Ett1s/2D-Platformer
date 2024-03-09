using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setvolume : MonoBehaviour
{
    private AudioSource audioSrc;
    public static float volume;

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
        volume = PlayerPrefs.GetFloat("volume");
        audioSrc.volume = volume;
    }
}
