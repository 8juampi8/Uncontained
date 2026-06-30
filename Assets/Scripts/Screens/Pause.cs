using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        musicSlider.onValueChanged.AddListener(ChangeMusic);
        sfxSlider.onValueChanged.AddListener(ChangeSfx);

        musicSlider.value = AudioManager.Instance.MusicVolume;
        sfxSlider.value = AudioManager.Instance.SfxVolume;
    }

    void ChangeMusic(float value)
    {
        AudioManager.Instance.ChangeMusicVolume(value);
    }

    void ChangeSfx(float value)
    {
        AudioManager.Instance.ChangeSFXVolume(value);
    }
}