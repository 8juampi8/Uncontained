using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System;

public class StartGame : Events
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D freeLight;

    [SerializeField] private GameObject startingPanel;
    [SerializeField] private GameObject hud;

    [SerializeField] private Movement_player movement;

    protected override void Start()
    {
        StartCoroutine(Titilar());
    }

    IEnumerator Titilar()
    {
        float intvl = 0.2f;

        while (startingPanel.activeSelf)
        {
            globalLight.color = Color.red;
            yield return new WaitForSeconds(intvl);

            globalLight.color = Color.white;
            yield return new WaitForSeconds(intvl);
        }

        globalLight.color = new Color(89f / 255f, 89f / 255f, 89f / 255f);
        globalLight.intensity = 0.01f;

        freeLight.enabled = true;
        movement.enabled = true;

        hud.SetActive(true);
    }
}