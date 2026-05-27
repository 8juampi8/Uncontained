using UnityEngine;

public class DefaultEventTutorial : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            panel.SetActive(true);
        }
    }
}