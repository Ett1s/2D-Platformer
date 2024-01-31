using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    public SoundArrays[] randSound;

    private AudioSource audioSrc => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 1f, bool random = false, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        audioSrc.pitch = Random.Range(p1, p2);
        audioSrc.PlayOneShot(clip, volume);
    }

    [System.Serializable]
    public class SoundArrays
    {
        public AudioClip[] soundArray;
    }
}