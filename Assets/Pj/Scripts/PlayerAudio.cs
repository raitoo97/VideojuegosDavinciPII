using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip trasformIntoDodgex;
    public AudioClip deTrasformIntoDodgex;
    public AudioClip roll;
    public List<AudioClip> walk = new List<AudioClip>(); 

    public void PlayTransform()
    {
        audioSource.PlayOneShot(trasformIntoDodgex);
    }
    public void PlayDetransform()
    {
        audioSource.PlayOneShot(deTrasformIntoDodgex);
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

    public void PlayWalk()
    {
        int randomNumber = Random.Range(0,walk.Count);
        audioSource.PlayOneShot(walk[randomNumber]);
    }
}
