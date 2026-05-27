using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightsOffTutorial : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D freeLight;

    [SerializeField] private GameObject powerSlider;

    [SerializeField] private GameObject panel;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            globalLight.color = new Color(53f / 255f, 53f / 255f, 53f / 255f);
            globalLight.intensity = 0.01f; 

            freeLight.enabled = true;

            panel.SetActive(true);
        }
    }
}