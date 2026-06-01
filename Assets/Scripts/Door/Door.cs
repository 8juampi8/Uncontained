using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour
{
    private ItemInteraction_player playerPickUp;
    [SerializeField] private string toScene;

    private GameObject player;
    private PlayerSoundsController soundsController;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null) return;

        playerPickUp = player.GetComponent<ItemInteraction_player>();
        soundsController = player.GetComponent<PlayerSoundsController>();
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
        soundsController.PlayDoor();

        yield return new WaitForSeconds(0.4f);

        Destroy(gameObject);

        SceneManager.LoadScene(toScene);
    }
}