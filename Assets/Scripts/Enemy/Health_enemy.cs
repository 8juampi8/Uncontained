using UnityEngine;
using System.Collections;

public class Health_enemy : MonoBehaviour
{
    private int health = 100;

    [SerializeField] private GameObject keyCard;
    [SerializeField] private GameObject item;

    private SpriteRenderer sprite;

    private PlayerSoundsController soundsController;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        soundsController = GameObject.FindWithTag("Player").GetComponent<PlayerSoundsController>();
    }

    public void getDamage(int damage)
    {
        health -= damage;

        soundsController.PlayHitEnemy();

        StartCoroutine(Damage());

        if (health <= 0)
        {
            Destroy(gameObject);

            GameManager.Instance.OffFollowing();

            soundsController.PlayDeathEnemy();

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