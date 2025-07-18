using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public List<AudioClip> talk = new List<AudioClip>();
    public AudioSource audioSource;
    void Update()
    {
        int chance = Random.Range(0, 100);
        if (chance <= 20)
        {
            StartCoroutine(ZombieTalk());
        }
    }

    public IEnumerator ZombieTalk()
    {
        int randomTiming = Random.Range(1,5);
        yield return new WaitForSeconds(randomTiming);
        int randomNumber = Random.Range(0, talk.Count);
        AudioClip clip = AudioManager.instance.ReturnSfxRandomPitch(talk[randomNumber]);
        audioSource.PlayOneShot(clip);
        yield return null;
    }
}
