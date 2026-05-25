using UnityEngine;

public class Guns_gun : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject cannon;

    [SerializeField] protected float shootCooldown;
    protected float shootTimer;

    [SerializeField] protected int gunCharger;
    public int GunCharger => gunCharger;

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
}