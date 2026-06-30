using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System;

public class StartGame : Events
{
    [SerializeField] private GameObject startingPanel;
    private GameObject hud;

    [SerializeField] private Movement_player movement;

    [SerializeField] private AudioClip alarmSound;

    protected override void Start()
    {
        hud = FindAnyObjectByType<HUD>(FindObjectsInactive.Include)?.gameObject;

        StartCoroutine(Titilar());
    }

    IEnumerator Titilar()
    {
        float intvl = 0.2f;

        if (startingPanel.activeSelf && alarmSound != null)
        {
            AudioManager.Instance.PlayMusic(alarmSound);
        }

        while (startingPanel.activeSelf)
        {
            globalLight.color = Color.red;
            yield return new WaitForSeconds(intvl);

            globalLight.color = Color.white;
            yield return new WaitForSeconds(intvl);

            globalLight.color = Color.black;
            yield return new WaitForSeconds(intvl);
        }

        AudioManager.Instance.StopMusic();
        globalLight.color = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        globalLight.intensity = 0.01f;

        freeLight.enabled = true;
        movement.enabled = true;

        hud.SetActive(true);
    }
}