using UnityEngine;

public class Events : MonoBehaviour
{
    protected GameObject player;

    [SerializeField] protected GameObject typePanel;
    [SerializeField] protected GameObject movementPanel;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            GameManager.Instance.SetSpawn(transform.position);
            Destroy(gameObject);
        }
    }
}