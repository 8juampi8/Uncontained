using UnityEngine;

public class Guns_gun : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    private GameObject cannon;
    [SerializeField] protected float shootCooldown;
    protected float shootTimer;
    [SerializeField] protected int maxCharger;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private WeaponType myType;

    public enum WeaponType { Pistol, Shotgun, SMG, Rifle }

    public AudioClip ShootSound=> shootSound;

    public WeaponType MyType => myType;
    public int MaxCharger => maxCharger;

    private int gunCharger;
    public int GunCharger => gunCharger;

    private int bulletsNedeed;

    private bool wasEquipped = false;
    public bool WasEquipped => wasEquipped;


    void Awake()
    {
        gunCharger = maxCharger;
    }

    public void UpdateShootTimer()
    {
        shootTimer += Time.deltaTime;
    }

    public void setAmmo(int ammo)
    {
        gunCharger = ammo;
    }

    public void setCannon(GameObject newCannon)
    {
        cannon = newCannon;
    }


    public void Shoot()
    {
        if (shootTimer <= shootCooldown) return;

        if (shootSound != null)
        {
            AudioManager.Instance.PlaySFX(shootSound);
        }

        Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);

        gunCharger--;
        InvManager.Instance.RemoveBullet();
        
        GameManager.Instance.UpdateAmmo();

        shootTimer = 0;
    }

    public void Reload()
    {
        if (gunCharger == maxCharger) return;

        bulletsNedeed = maxCharger - gunCharger;

        int bulletsToReload = 0;

        switch (myType)
        {
            case WeaponType.Pistol:
            case WeaponType.SMG:
                bulletsToReload = Mathf.Min(bulletsNedeed, InvManager.Instance.SmallAmmo);
                break;
            case WeaponType.Shotgun:
                bulletsToReload = Mathf.Min(bulletsNedeed, InvManager.Instance.ShotgunAmmo);
                break;
            case WeaponType.Rifle:
                bulletsToReload = Mathf.Min(bulletsNedeed, InvManager.Instance.RifleAmmo);
                break;
        }

        if (bulletsToReload <= 0) return;

        gunCharger += bulletsToReload;
        InvManager.Instance.AddBullet(bulletsToReload);
        InvManager.Instance.UseAmmo(bulletsToReload);

        GameManager.Instance.UpdateAmmo();
        GameManager.Instance.UpdateMoreAmmo();
        AudioManager.Instance.PlaySFX(reloadSound);
    }
}
