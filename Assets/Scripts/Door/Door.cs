using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private ItemInteraction_player player;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.HasKeyCard)
        {
            Destroy(gameObject);

            GameManager.instance.Win();
        }
    }
}