using UnityEngine;

public class Shotgun_gun : Guns_gun
{
    private float shootCooldown = 1.25f;
    private float shootTimer = 0;

    // MODIFICO EL DISPARO

    void Update()
    {
        shootTimer += Time.deltaTime;
    }
    // MODIFICO EL DISPARO
    public override void Shoot()
    {
        if (shootTimer >= shootCooldown)
        {
            Instantiate(bullet, canion.transform.position, canion.transform.rotation);
            shootTimer = 0;
        }
    }
}