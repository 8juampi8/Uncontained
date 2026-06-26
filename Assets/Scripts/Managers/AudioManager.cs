using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private float musicVolume;
    private float sfxVolume;

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

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
