using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    [SerializeField] private AudioSource generalSource;
    public AudioSource GeneralSource => generalSource;

    [SerializeField] private AudioSource weaponsSource;
    [SerializeField] private AudioSource footstepsSource;

    [SerializeField] private AudioClip pistolShoot;
    public AudioClip PistolShoot => pistolShoot;

    [SerializeField] private AudioClip shotgunShoot;
    public AudioClip ShotgunShoot => shotgunShoot;

    [SerializeField] private AudioClip smgShoot;
    public AudioClip SmgShoot => smgShoot;

    [SerializeField] private AudioClip rifleShoot;
    public AudioClip RifleShoot => rifleShoot;

    [SerializeField] private AudioClip footsteps;

    [SerializeField] private AudioClip alarm;
    public AudioClip Alarm => alarm;

    [SerializeField] private AudioClip getDamage;
    public AudioClip GetDamage => getDamage;

    [SerializeField] private AudioClip death;
    public AudioClip Death => death;

    [SerializeField] private AudioClip pickKey;

    [SerializeField] private AudioClip pickAmmo;

    [SerializeField] private AudioClip pickBattery;

    [SerializeField] private AudioClip toggleFL;

    [SerializeField] private AudioClip silence;

    [SerializeField] private AudioClip door;

    [SerializeField] private AudioClip reload;

    [SerializeField] private AudioClip count;

    [SerializeField] private AudioClip explotion;

    // GENERALES

    public void PlayOneShot(AudioSource source, AudioClip sound)
    {
        source.PlayOneShot(sound);
    }

    public void PlayLoop(AudioSource source, AudioClip sound)
    {
        if (!source.isPlaying)
        {
            source.clip = sound;
            source.loop = true;
            source.Play();
        }
    }

    public void Stop(AudioSource source)
    {
        source.Stop();
    }

    // INDIVIDUALES

    public void PlayShootSound(AudioClip sound)
    {
        PlayOneShot(weaponsSource, sound);
    }

    public void PlayFootsteps()
    {
        PlayLoop(footstepsSource, footsteps);
    }

    public void StopFootsteps()
    {
        Stop(footstepsSource);
    }

    public void PlayPickKey()
    {
        PlayOneShot(generalSource, pickKey);
    }

    public void PlayPickAmmo()
    {
        PlayOneShot(generalSource, pickAmmo);
    }

    public void PlayPickBattery()
    {
        PlayOneShot(generalSource, pickBattery);
    }

    public void PlayToggleFL()
    {
        PlayOneShot(generalSource, toggleFL);
    }

    public void PlaySilence()
    {
        PlayOneShot(generalSource, silence);
    }

    public void PlayDoor()
    {
        PlayOneShot(generalSource, door);
    }

    public void PlayReload()
    {
        PlayOneShot(generalSource, reload);
    }

    public void PlayCount()
    {
        PlayOneShot(generalSource, count);
    }

    public void PlayExplotion()
    {
        PlayOneShot(generalSource, explotion);
    }

    //VOLUMEN
    public void ChangeSFXGeneral(float volume)
    {
        generalSource.volume = volume;
    }
    public void ChangeSFXWeapons(float volume)
    {
        weaponsSource.volume = volume;
    }
    public void ChangeSFXFootSteps(float volume)
    {
        footstepsSource.volume = volume;
    }
}