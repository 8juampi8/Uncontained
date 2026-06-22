using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEvents_player : Events
{

    [SerializeField] private GameObject wallTut;
    [SerializeField] private GameObject panel;

    [SerializeField] private GameObject[] hudItem;

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
            if(globalLight != null){
                globalLight.color = Color.black;
                globalLight.intensity = 0.01f;
            }

            if(freeLight != null) freeLight.enabled = true;
            
            GameManager.Instance.SetSpawn(transform.position);

            panel.SetActive(true);
            typePanel.SetActive(true);

            wallTut.SetActive(true);

            silence.ResetWait();

            for (int i = 0; i < hudItem.Length; i++)
            {
                hudItem[i].SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
