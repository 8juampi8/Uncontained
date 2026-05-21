using UnityEngine;

public abstract class Guns_gun : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject cannon;

    [SerializeField] private string gunID;
    public string GunID => gunID;

    protected bool isEquipped = false;

    [SerializeField] private GameObject pickupPrefab;
    public GameObject PickupPrefab => pickupPrefab;

    public abstract void Shoot();

    public virtual void Equip()
    {
        isEquipped = true;
    }

    public virtual void Drop()
    {
        isEquipped = false;

        Destroy(gameObject);
    }
}