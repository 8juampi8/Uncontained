using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class DefaultEventTutorial : Events
{
    [SerializeField] private GameObject panel;

    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D freeLight;

    [SerializeField] private GameObject[] hudItem;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            GameManager.Instance.SetSpawn(transform.position);

            globalLight.color = new Color(0f / 255f, 0f / 255f, 0f / 255f);
            globalLight.intensity = 0.01f;

            freeLight.enabled = true;

            for (int i = 0; i < hudItem.Length; i++)
            {
                hudItem[i].SetActive(true);
            }

            panel.SetActive(true);
            typePanel.SetActive(true);

            if (typePanel != movementPanel)
            {
                movementPanel.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}