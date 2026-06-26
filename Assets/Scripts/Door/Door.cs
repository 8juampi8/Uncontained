using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour
{
    private ItemInteraction_player playerPickUp;
    [SerializeField] private string toScene;

    [SerializeField] private AudioClip doorSound;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null) return;

        playerPickUp = player.GetComponent<ItemInteraction_player>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerPickUp.HasKeyCard)
        {
            StartCoroutine(WaitUntilOpen());
        }
    }

    IEnumerator WaitUntilOpen()
    {
        if (AudioManager.Instance != null && doorSound != null)
        {
            AudioManager.Instance.PlaySFX(doorSound);
        }

        yield return new WaitForSeconds(0.4f);

        Destroy(gameObject);
        SceneManager.LoadScene(toScene);
    }
}