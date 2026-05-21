using UnityEngine;

public class Shotgun_gun : Guns_gun
{
    private float shootCooldown = 1.25f;
    private float shootTimer = 1.25f;

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
            Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);
            shootTimer = 0;
        }
    }
}