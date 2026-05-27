using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEvents_player : MonoBehaviour
{

    [SerializeField] private GameObject wallTut;

    SilenceHab_player silence;

    void Start()
    {
        silence = GameObject.FindWithTag("Player").GetComponent<SilenceHab_player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            wallTut.SetActive(true);

            silence.ResetWait();
        }
    }
}
