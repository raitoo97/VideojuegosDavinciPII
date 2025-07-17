using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip trasformIntoDodgex;
    public AudioClip roll;

    public void PlayTransform()
    {
        audioSource.PlayOneShot(trasformIntoDodgex);
    }

    public void RollLoop()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = roll;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    public void StopRoll()
    {
        audioSource.Stop();
        audioSource.loop=false;
    }
}
