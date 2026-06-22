using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class DefaultEventTutorial : Events
{
    [SerializeField] private GameObject panel;

    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D freeLight;

    [SerializeField] private GameObject wall;

    [SerializeField] private GameObject[] hudItem;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            movementPanel.SetActive(false);
            typePanel.SetActive(true);

            panel.SetActive(true);
            GameManager.Instance.SetSpawn(transform.position);

            if (wall != null)
                wall.SetActive(true);

            globalLight.color = Color.black;
            globalLight.intensity = 0.01f;

            freeLight.enabled = true;

            for (int i = 0; i < hudItem.Length; i++)
            {
                hudItem[i].SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}