using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Guns_gun : Item
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject cannon;
    private GameObject player;
    private GameObject gunPosition;

    public abstract void Shoot();

    public override void Equip()
    {
        player = GameObject.FindWithTag("Player");
        gunPosition = GameObject.FindWithTag("GunPos");

        transform.SetParent(gunPosition.transform);
        transform.localPosition = Vector2.zero;
        transform.rotation = player.transform.rotation;
    }

    public override void Drop()
    {
        transform.SetParent(null);
    }
}