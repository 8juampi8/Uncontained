using UnityEngine;

public class Collision_bullet : MonoBehaviour
{
    [SerializeField] private int damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Health_enemy>().getDamage(damage);
            Destroy(gameObject);
        }
    }
}