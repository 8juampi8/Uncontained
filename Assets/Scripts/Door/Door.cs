using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private ItemInteraction_player player;
    [SerializeField] private string toScene;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.HasKeyCard)
        {
            Destroy(gameObject);

            SceneManager.LoadScene(toScene);
        }
    }
}