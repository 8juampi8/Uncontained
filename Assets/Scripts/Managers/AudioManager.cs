using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private GameObject musicObj;
    [SerializeField] private GameObject sfxObj;

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

    public void ChangeMusicVolume()
    {
        Slider musicSlider = musicObj.GetComponent<Slider>();
        musicSource.volume = musicSlider.value;
    }

    public void ChangeSFXVolume()
    {
        Slider sfxSlider = sfxObj.GetComponent<Slider>();
        sfxSource.volume = sfxSlider.value;
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
