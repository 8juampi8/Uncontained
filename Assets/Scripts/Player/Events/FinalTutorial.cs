using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalTutorial : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            SceneManager.LoadScene("Menu");
        }
    }
}