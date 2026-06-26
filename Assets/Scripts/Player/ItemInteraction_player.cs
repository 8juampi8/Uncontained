using UnityEngine;

public class ItemInteraction_player : MonoBehaviour
{
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private AudioClip pickAmmoSound;
    [SerializeField] private AudioClip pickKeySound;
    [SerializeField] private AudioClip pickBatterySound;

    private bool hasKeyCard = false;
    public bool HasKeyCard => hasKeyCard;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D currentItem = Physics2D.OverlapCircle(transform.position, 1.5f, itemLayer);

            if (currentItem == null) return;

            Item item = currentItem.GetComponent<Item>();

            if (item == null) return;

            switch (item.tag)
            {
                case "Gun":
                    if (InvManager.Instance.IsEquipped)
                    {
                        InvManager.Instance.DropGun();
                    }

                    Guns_gun floorGunScript = item.GetComponent<Guns_gun>();
                    if (floorGunScript != null)
                    {
                        var cachedGun = InvManager.Instance.GetGunScript(item.ItemName);
                        if (cachedGun != null)
                        {
                            cachedGun.setAmmo(floorGunScript.GunCharger);
                        }
                    }

                    InvManager.Instance.AddItem(item.ItemName);
                    InvManager.Instance.EquipGun();

                    if (pickAmmoSound != null) AudioManager.Instance.PlaySFX(pickAmmoSound);

                    break;

                case "KeyCard":
                    hasKeyCard = true;

                    if (pickKeySound != null) AudioManager.Instance.PlaySFX(pickKeySound);

                    break;

                case "Battery":
                    if (flashlight == null) return;
                    flashlight.AddPower();

                    if (pickBatterySound != null) AudioManager.Instance.PlaySFX(pickBatterySound);

                    break;

                case "PistolBullet":
                    InvManager.Instance.PickPistolAmmo();
                    GameManager.Instance.UpdateMoreAmmo();

                    if (pickAmmoSound != null) AudioManager.Instance.PlaySFX(pickAmmoSound);

                    break;

                case "ShotgunBullet":
                    InvManager.Instance.PickShotgunAmmo();
                    GameManager.Instance.UpdateMoreAmmo();

                    if (pickAmmoSound != null) AudioManager.Instance.PlaySFX(pickAmmoSound);

                    break;
                case "RifleBullet":
                    InvManager.Instance.PickRifleAmmo();
                    GameManager.Instance.UpdateMoreAmmo();

                    if (pickAmmoSound != null) AudioManager.Instance.PlaySFX(pickAmmoSound);

                    break;
                case "Button":
                    Button btnEvent = GameObject.FindWithTag("ButtonEvent").GetComponent<Button>();
                    btnEvent.StartCount();

                    break;
            }

            Destroy(item.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (InvManager.Instance.IsEquipped)
            {
                InvManager.Instance.DropGun();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (flashlight == null) return;
            flashlight.Toggle();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (InvManager.Instance.Obj != null)
            {
                InvManager.Instance.Obj.GetComponent<Guns_gun>().Reload();
            }
        }
    }
}