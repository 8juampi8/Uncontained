using System;
using UnityEngine;

public class Pistol_gun : Guns_gun
{
    void Update()
    {
        shootTimer += Time.deltaTime;
    }
}