using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Pickitem_player keyCard;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && keyCard.HasKeyCard)
        {
            Destroy(gameObject);

            GameManager.instance.Win();
        }
    }
}