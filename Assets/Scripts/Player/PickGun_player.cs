using TMPro;
using UnityEngine;

public class PickGun_player : MonoBehaviour
{
    private bool onGun = false;
    private GameObject gun;
    [SerializeField] private GameObject gunPosition;
    private bool gunEquiped = false;
    public bool equiped => gunEquiped;

    void Update()
    {
        // SI APRETO LA E Y ESTA ENCIMA DE UN ARMA, LA EQUIPA
        if (Input.GetKeyDown(KeyCode.E) && onGun && !gunEquiped)
        {
            OnEquipGun(gun);
            gunEquiped = true;
        }

        // SI APRETA LA G MIENTRAS TIENE UN ARMA EQUIPADA, LA SUELTA
        if (Input.GetKeyDown(KeyCode.G) && gunEquiped)
        {
            OnDropGun(GameManager.instance.EquippedGun);
            gunEquiped = false;
        }
    }

    // VER SI ESTA ENCIMA DE UN ARMA
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            onGun = true;
            gun = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            onGun = false;
            gun = null;
        }
    }

    // EQUIPAR EL ARMA
    void OnEquipGun(GameObject gun)
    {
        GameManager.instance.PickGun(gun);

        gun.transform.SetParent(gunPosition.transform);
        gun.transform.localPosition = Vector2.zero;
        gun.transform.rotation = transform.rotation;

        Guns_gun pickedGun = gun.GetComponent<Guns_gun>();
        if (pickedGun != null)
        {
            pickedGun.OnEquip();
        }
    }

    // SOLTAR EL ARMA
    void OnDropGun(GameObject gun)
    {
        Guns_gun droppedGun = gun.GetComponent<Guns_gun>();
        if (droppedGun != null)
        {
            droppedGun.OnDrop();
        }

        gun.transform.SetParent(null);
        GameManager.instance.DropGun();
    }
}