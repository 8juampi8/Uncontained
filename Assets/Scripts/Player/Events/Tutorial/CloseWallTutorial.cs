using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEvents_player : Events
{

    [SerializeField] private GameObject wallTut;
    [SerializeField] private GameObject panel;

    SilenceHab_player silence;

    protected override void Start()
    {
        base.Start();

        silence = GameObject.FindWithTag("Player").GetComponent<SilenceHab_player>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            panel.SetActive(true);
            wallTut.SetActive(true);

            silence.ResetWait();
            Destroy(gameObject);
        }
    }
}
