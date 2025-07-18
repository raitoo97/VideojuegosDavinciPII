using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> walk = new List<AudioClip>(); //heavy military boots
    public AudioClip run;
    public AudioClip punch; //Big Claws
    public AudioClip invokeZombies; //from the earth, laugh?
    public AudioClip rawr1; //screech / scream
    public AudioClip rawr; // searching Player
    public AudioClip dead; //On his knees, then fall to the floor.
    public AudioClip idleTurret; 
    public void PlayWalk()
    {
        int randomNumber = Random.Range(0,walk.Count);
        audioSource.PlayOneShot(walk[randomNumber]);
    }
}
