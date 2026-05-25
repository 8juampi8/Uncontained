using UnityEngine;

public class Shotgun_gun : Guns_gun
{
    void Update()
    {
        shootTimer += Time.deltaTime;
    }
}