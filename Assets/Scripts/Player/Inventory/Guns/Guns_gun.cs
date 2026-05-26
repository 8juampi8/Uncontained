using UnityEngine;

public class Guns_gun : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject cannon;

    [SerializeField] protected float shootCooldown;
    protected float shootTimer;

    [SerializeField] protected int maxCharger;

    private int gunCharger;
    public int GunCharger => gunCharger;

    private int bulletsNedeed;

    void Awake()
    {
        gunCharger = maxCharger;
    }

    public void setAmmo(int ammo)
    {
        gunCharger = ammo;
    }

    public void Shoot()
    {
        if(shootTimer <= shootCooldown) return;

        Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);

        gunCharger--;
        InvManager.Instance.RemoveBullet();
        Debug.Log(gunCharger);


        shootTimer = 0;
    }

    public void Reload()
    {
        if(gunCharger == maxCharger) return;

        bulletsNedeed = maxCharger - gunCharger;

        Pistol_gun pistol = InvManager.Instance.Obj.GetComponent<Pistol_gun>();

        int bulletsToReload;

        if (pistol != null)
        {
            bulletsToReload = Mathf.Min(bulletsNedeed, InvManager.Instance.PistolAmmo);       
        }
        else
        {
            bulletsToReload = Mathf.Min(bulletsNedeed, InvManager.Instance.ShotgunAmmo);
        }

        if(bulletsToReload <= 0)
        {
            Debug.Log("No tenes balas para poder recargar");
            return;
        }

        gunCharger += bulletsToReload;
        InvManager.Instance.AddBullet(bulletsToReload);

        InvManager.Instance.UseAmmo(bulletsToReload);
        Debug.Log("Recargaste");
    }
}