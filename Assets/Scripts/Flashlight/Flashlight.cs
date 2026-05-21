using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    Light2D lght;

    void Start()
    {
        lght = GetComponent<Light2D>();
    }

    public void Toggle()
    {
        if (Time.timeScale > 0)
        {
            lght.enabled = !lght.enabled;
        }
    }
}