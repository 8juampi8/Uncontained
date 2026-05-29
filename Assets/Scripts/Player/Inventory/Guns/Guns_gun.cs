using UnityEngine;

public class Guns_gun : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    private GameObject cannon;

    [SerializeField] protected float shootCooldown;
    protected float shootTimer;

    [SerializeField] protected int maxCharger;
    public int MaxCharger => maxCharger;

    private int gunCharger;
    public int GunCharger => gunCharger;

    private int bulletsNedeed;

    private bool wasEquipped = false;
    public bool WasEquipped => wasEquipped;

    // CAMBIADO: Usamos Awake para establecer las balas iniciales por defecto antes de que el InvManager las lea
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

        Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);

        gunCharger--;
        InvManager.Instance.RemoveBullet();
        Debug.Log(gunCharger);

        GameManager.Instance.UpdateAmmo();

        shootTimer = 0;
    }

    public void Reload()
    {
        if (gunCharger == maxCharger) return;

        bulletsNedeed = maxCharger - gunCharger;

        int bulletsToReload = 0;

        if (InvManager.Instance.Obj.GetComponent<Pistol_gun>() != null || InvManager.Instance.Obj.GetComponent<SMG_gun>() != null)
        {
            bulletsToReload = Mathf.Min(bulletsNedeed, InvManager.Instance.SmallAmmo);
        }
        if (InvManager.Instance.Obj.GetComponent<Shotgun_gun>() != null)
        {
            bulletsToReload = Mathf.Min(bulletsNedeed, InvManager.Instance.ShotgunAmmo);
        }
        if (InvManager.Instance.Obj.GetComponent<Rifle_gun>() != null)
        {
            bulletsToReload = Mathf.Min(bulletsNedeed, InvManager.Instance.RifleAmmo);
        }

        if (bulletsToReload <= 0)
        {
            Debug.Log("No tenes balas para poder recargar");
            return;
        }

        gunCharger += bulletsToReload;
        InvManager.Instance.AddBullet(bulletsToReload);

        InvManager.Instance.UseAmmo(bulletsToReload);
        Debug.Log("Recargaste");

        GameManager.Instance.UpdateAmmo();
        GameManager.Instance.UpdateMoreAmmo();
    }
}