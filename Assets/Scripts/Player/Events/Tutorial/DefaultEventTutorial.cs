using UnityEngine;

public class DefaultEventTutorial : Events
{
    [SerializeField] private GameObject panel;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            panel.SetActive(true);
        }
    }
}