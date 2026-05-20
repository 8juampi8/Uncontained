using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEvents_player : MonoBehaviour
{

    [SerializeField] private GameObject wallTutorial;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            float newScaley = 9.4f;
            float diference = newScaley - wallTutorial.transform.localScale.y;

            Vector3 scale = wallTutorial.transform.localScale;
            scale.y = newScaley;
            wallTutorial.transform.localScale = scale;

            wallTutorial.transform.localPosition -= new Vector3(0, diference / 2f, 0);
        }
    }
}
