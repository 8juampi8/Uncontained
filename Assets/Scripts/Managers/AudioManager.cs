using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    // Apartes
    // private AudioSource enemySFX;
    private AudioSource footstepsSFX;

    private float musicVolume = 1;
    public float MusicVolume => musicVolume;

    private float sfxVolume = 1;
    public float SfxVolume => sfxVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetFootstepsSource(AudioSource source)
    {
        footstepsSFX = source;
        footstepsSFX.volume = sfxVolume;
    }

    // public void SetEnemySource(AudioSource source)
    // {
    //     enemySFX = source;
    //     enemySFX.volume = sfxVolume;
    // }

    public void ChangeMusicVolume(float vol)
    {
        musicSource.volume = vol;
        musicVolume = vol;
    }

    public void ChangeSFXVolume(float vol)
    {
        sfxSource.volume = vol;
        if(footstepsSFX != null)
        {
            footstepsSFX.volume = vol;
        }
        // if(enemySFX != null)
        // {
        //     enemySFX.volume = vol;
        // }
        sfxVolume = vol;
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
