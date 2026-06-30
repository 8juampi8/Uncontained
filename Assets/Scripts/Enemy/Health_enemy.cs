using UnityEngine;
using System.Collections;

public class Health_enemy : MonoBehaviour
{
    private int health = 100;

    [SerializeField] private GameObject keyCard;
    [SerializeField] private GameObject pistolBullet;
    [SerializeField] private GameObject shotgunBullet;
    [SerializeField] private GameObject rifleBullet;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void getDamage(int damage)
    {
        health -= damage;

        if (AudioManager.Instance != null && hitSound != null)
            AudioManager.Instance.PlaySFX(hitSound);

        StartCoroutine(Damage());

        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.OffFollowing();

            if (AudioManager.Instance != null && deathSound != null)
                AudioManager.Instance.PlaySFX(deathSound);

            if (keyCard != null)
            {
                Instantiate(keyCard, transform.position, transform.rotation);
            }
            if (InvManager.Instance.CurrentGun != null)
            {
                switch (InvManager.Instance.CurrentGun.MyType)
                {
                    case Guns_gun.WeaponType.Pistol:
                    case Guns_gun.WeaponType.SMG:
                        Instantiate(pistolBullet, transform.position, transform.rotation);
                        break;
                    case Guns_gun.WeaponType.Shotgun:
                        Instantiate(shotgunBullet, transform.position, transform.rotation);
                        break;
                    case Guns_gun.WeaponType.Rifle:
                        Instantiate(rifleBullet, transform.position, transform.rotation);
                        break;
                }
            }
        }
    }

    IEnumerator Damage()
    {
        float dly = 0.2f;

        sprite.color = Color.red;
        yield return new WaitForSeconds(dly);
        sprite.color = Color.white;
    }
}