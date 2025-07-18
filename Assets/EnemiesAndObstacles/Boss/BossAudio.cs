using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> walk = new List<AudioClip>(); //heavy military boots
    public List<AudioClip> attack = new List<AudioClip>(); //Big Claws
    public List<AudioClip> talk = new List<AudioClip>(); 
    public AudioClip invokeZombies; //from the earth, laugh?
    public AudioClip rawr1; //screech / scream
    public AudioClip dead; //On his knees, then fall to the floor.
    public void PlayWalk()
    {
        int randomNumber = Random.Range(0,walk.Count);
        audioSource.PlayOneShot(walk[randomNumber]);
    }

    public void PlayAttack() 
    {
        int randomNumber = Random.Range(0, attack.Count);
        audioSource.PlayOneShot(attack[randomNumber]);
    }

    public void PlayInvokeZombies()
    {
        audioSource.PlayOneShot(invokeZombies);
    }

    public void PlayRawr() 
    {
        audioSource.PlayOneShot(rawr1);
    }

    public void PlayTalk()
    {
        int randomNumnber = Random.Range(0,talk.Count);
        audioSource.PlayOneShot(talk[randomNumnber]);
    }
}
