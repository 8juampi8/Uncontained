using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Guns_gun : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject canion;
    [SerializeField] private Collider2D gunCollider;
    // public enum Guns
    // {
    //     Pistol,
    //     Shotgun,
    //     Rifle,
    //     SMG
    // }

    // [SerializeField] private Guns gunType;
    // public Guns GunType => gunType;

    // CREO FUNCION DE DISPARO Y LA MODIFICO EN CADA TIPO DE ARMA
    public abstract void Shoot();
    public void OnEquip()
    {
        if (gunCollider != null)
            gunCollider.enabled = false;
    }
    public void OnDrop()
    {
        if (gunCollider != null)
            gunCollider.enabled = true;
    }
}