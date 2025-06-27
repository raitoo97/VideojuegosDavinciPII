using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    //Zombie Audio
    [SerializeField] public AudioClip[] zombieAttackSfx;
    [SerializeField] public AudioClip[] missileImpactSfx;
    [SerializeField] public AudioClip[] playerDamageSfx;
    [SerializeField] public AudioClip[] turretPlayerImpactSfx;
    [SerializeField] public AudioClip[] skillPlayerDash;
    [SerializeField] public AudioClip   EnemyTurretShot;
    public AudioClip Level1Music;
    public AudioClip buttonClick;
    public List<AudioSource> audioSources = new List<AudioSource>();
    private bool isMusicPlaying;
    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        CompleteAudioSource(10);
    }
    // MUTEA EL SONIDO
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) ToggleMusic();
    }
    public void PlaySfx(AudioClip clip)
    {
        var audioSource = GetSource();
        if(audioSource == null || clip == null) return;
        audioSource.PlayOneShot(clip);
    }
    public void CompleteAudioSource(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var newSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(newSource);
        }
    }
    public AudioSource GetSource()
    {
        return audioSources.Find(x => x.isPlaying == false);
    }
    public AudioSource GetSourceSpecific(AudioClip clip)
    {
        var au = audioSources.Find(x => x.clip == clip);
        if (au == null) return null;
        return au;
    }
    public void PlaySfxRandomPitch(AudioClip clip) 
    {
        var audioSource = GetSource();
        if (audioSource == null || clip == null) return;
        audioSource.pitch = Random.Range(1f, 1.2f);
        audioSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        var audioSource = GetSource();
        if (audioSource == null || clip == null) return;
        audioSource.PlayOneShot(clip);
    }
    public void PauseMusicClip(AudioClip clip)
    {
        var au = GetSourceSpecific(clip);
        if (au == null) return;
        au.Stop();
    }
    private void ToggleMusic()
    {
        isMusicPlaying=!isMusicPlaying;
        if (isMusicPlaying)
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.mute = false;
            }
        }
        else
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.mute = true;
            }
        }
    }
}
