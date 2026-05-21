using UnityEngine;

public class GunPickup : Item
{
    [SerializeField] private Guns_gun gunPrefab;

    public Guns_gun GunPrefab => gunPrefab;
}