using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Guns_gun : Item
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject cannon;
    private GameObject player;
    private GameObject gunPosition;
    protected bool isEquipped = false;

    public abstract void Shoot();

    public override void Equip()
    {
        isEquipped = true;
    }

    public void Drop()
    {
        transform.SetParent(null);

        isEquipped = false;

        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = true;
    }
}