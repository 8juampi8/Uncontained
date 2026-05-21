using System;
using UnityEngine;

public class Pistol_gun : Guns_gun
{
    private float shootCooldown = 0.5f;
    private float shootTimer = 0.5f;

    // MODIFICO EL DISPARO

    void Update()
    {
        shootTimer += Time.deltaTime;
    }
    public override void Shoot()
    {
        if (shootTimer >= shootCooldown)
        {
            Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);
            shootTimer = 0;
        }
    }
}