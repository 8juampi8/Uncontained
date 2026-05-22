using UnityEngine;

public abstract class Guns_gun : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject cannon;

    public abstract void Shoot();
}