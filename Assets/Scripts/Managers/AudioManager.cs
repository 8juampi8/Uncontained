using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

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

    public void ChangeMusicVolume(float vol)
    {
        musicSource.volume = vol;
        musicVolume = vol;
    }

    public void ChangeSFXVolume(float vol)
    {
        sfxSource.volume = vol;
        sfxVolume = vol;
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
