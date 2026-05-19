using UnityEngine;

public class Collision_bullet : MonoBehaviour
{
    [SerializeField] private int damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health_enemy>().getDamage(damage);
        }

        Destroy(gameObject);
    }
}