using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEvents_player : MonoBehaviour
{

    [SerializeField] private GameObject wallTut;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            wallTut.SetActive(true);
        }
    }
}
