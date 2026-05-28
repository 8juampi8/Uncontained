using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightsOffTutorial : Events
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D freeLight;

    [SerializeField] private GameObject powerSlider;

    [SerializeField] private GameObject panel;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            globalLight.color = new Color(0f / 255f, 0f / 255f, 0f / 255f);
            globalLight.intensity = 0.01f;

            freeLight.enabled = true;

            panel.SetActive(true);
            Destroy(gameObject);
        }
    }
}