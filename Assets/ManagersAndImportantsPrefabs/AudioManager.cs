using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioSource> audioSources = new List<AudioSource>();
    //AudioMixer
    public AudioMixer audioMixer;
    public AudioMixerGroup masterGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;
    //Zombie Audio
    [SerializeField]public AudioClip[]zombieAttackSfx;
    [SerializeField]public AudioClip[]missileImpactSfx;
    [SerializeField]public AudioClip[]playerDamageSfx;
    [SerializeField]public AudioClip[]turretPlayerImpactSfx;
    [SerializeField]public AudioClip[]skillPlayerDash;
    [SerializeField]public AudioClip EnemyTurretShot;
    //UI
    [SerializeField]public AudioClip UpgradeSkill;
    [SerializeField]public AudioClip UnlockSkill;
    [SerializeField] public AudioClip CantUnlockSkill;

    //Beam Obstacle
    [SerializeField] public AudioClip BeamSfx;
    public AudioClip Level1Music;
    public AudioClip buttonClick;
    public AudioClip bossFight;
    [Header("UI")]
    [SerializeField]private Slider _masterSlider;
    [SerializeField]private float _initMasterVolumen = 0.5f;
    [SerializeField]private Slider _musicSlider;
    [SerializeField]private float _initMusicVolumen = 0.5f;
    [SerializeField]private Slider _sfxSlider;
    [SerializeField] private float _initSfxVolumen = 0.5f;
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
        _masterSlider.value = _initMasterVolumen;
        SetMasterVolume(_initMasterVolumen);
        _musicSlider.value = _initMusicVolumen;
        SetMusicVolume(_initMusicVolumen);
        _sfxSlider.value = _initSfxVolumen;
        SetSfxVolume(_initSfxVolumen);
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
    public void PlaySfx(AudioClip clip)
    {
        var audioSource = GetSource();
        if (audioSource == null || clip == null) return;
        audioSource.outputAudioMixerGroup = sfxGroup;
        audioSource.PlayOneShot(clip);
    }
    public void PlaySfxRandomPitch(AudioClip clip) 
    {
        var audioSource = GetSource();
        if (audioSource == null || clip == null) return;
        audioSource.outputAudioMixerGroup = sfxGroup;
        audioSource.pitch = Random.Range(1f, 1.2f);
        audioSource.PlayOneShot(clip);
    }

    public AudioClip ReturnSfxRandomPitch(AudioClip clip)
    {
        var audioSource = GetSource();
        if (audioSource == null || clip == null) return null;
        audioSource.outputAudioMixerGroup = sfxGroup;
        audioSource.pitch = Random.Range(1f, 1.2f);
        return clip;
    }
    public void PlayMusic(AudioClip clip)
    {
        var audioSource = GetSource();
        if (audioSource == null || clip == null) return;
        audioSource.outputAudioMixerGroup = musicGroup;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Stop()
    {
        var audioSource = GetSource();
        if (audioSource == null) return;
        audioSource.Stop();
    }
    public void PauseClip(AudioClip clip)
    {
        var au = audioSources.Find(x => x.clip == clip);
        if (au == null) return;
        au.Stop();
    }
    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }
    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }
    public void SetSfxVolume(float value)
    {
        audioMixer.SetFloat("Sfx", Mathf.Log10(value) * 20);
    }
}
