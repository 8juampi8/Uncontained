using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class DefaultEventTutorial : Events
{
    [SerializeField] private GameObject panel;

    [SerializeField] private GameObject wall;

    [SerializeField] private GameObject[] hudItem;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {   
            if(globalLight != null){
                globalLight.color = Color.black;
                globalLight.intensity = 0.01f;
            }

            if(freeLight != null) freeLight.enabled = true;

            typePanel.SetActive(true);
            panel.SetActive(true);

            GameManager.Instance.SetSpawn(transform.position);

            if (wall != null) wall.SetActive(true);

            for (int i = 0; i < hudItem.Length; i++)
            {
                hudItem[i].SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}