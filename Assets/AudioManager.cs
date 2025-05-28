using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;

    //Zombie Audio
    [SerializeField] public AudioClip[] zombieAttackSfx;
    [SerializeField] public AudioClip[] missileImpactSfx;
    [SerializeField] public AudioClip[] playerDamageSfx;



    private bool isMusicPlaying;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

   // MUTEA EL SONIDO
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) ToggleMusic();
    }


    public void PlaySfx(AudioClip clip)
    {
        if (clip != null)
        {
             sfxAudioSource.PlayOneShot(clip); 
        }
    }

    public void PlaySfxRandomPitch(AudioClip clip) 
    {
        sfxAudioSource.pitch = Random.Range(1f, 1.2f);
        if (clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }

    public bool GetIsMusicPlaying() {  return isMusicPlaying; }


    private void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
    }
}
