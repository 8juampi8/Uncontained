using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEvents_player : Events
{

    [SerializeField] private GameObject wallTut;

    SilenceHab_player silence;

    void Start()
    {
        silence = GameObject.FindWithTag("Player").GetComponent<SilenceHab_player>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            wallTut.SetActive(true);

            silence.ResetWait();
        }
    }
}
