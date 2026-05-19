using UnityEngine;

public class Health_enemy : MonoBehaviour
{
    private int health = 100;

    [SerializeField] private GameObject keyCard;

    public void getDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);

            if (keyCard != null)
            {
                Instantiate(keyCard, transform.position, transform.rotation);
            }
        }
    }
}