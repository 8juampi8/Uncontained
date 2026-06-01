using UnityEngine;

public class Events : MonoBehaviour
{
    protected GameObject player;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Destroy(gameObject);
        }
    }
}