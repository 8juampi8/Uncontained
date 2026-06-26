using UnityEngine;
using System.Collections;

public class Health_enemy : MonoBehaviour
{
    private int health = 100;

    [SerializeField] private GameObject keyCard;
    [SerializeField] private GameObject item;
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
            if (item != null)
            {
                Instantiate(item, transform.position, transform.rotation);
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