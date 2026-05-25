using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    private float power;
    Light2D lght;

    void Start()
    {
        lght = GetComponent<Light2D>();

        power = GameManager.Instance.flashlightPower;
    }

    public void Toggle()
    {
        if (Time.timeScale > 0)
        {
            lght.enabled = !lght.enabled;
        }
    }

    void Update()
    {
        FlashlightLife();
    }

    private void FlashlightLife()
    {
        if (power <= 0)
        {
            power = 0;
            lght.enabled = false;
        }

        if (lght.enabled && power > 0)
        {
            power -= Time.deltaTime;
        }
        GameManager.Instance.flashlightPower = power;
    }

    public void AddPower()
    {
        power += 25;
        GameManager.Instance.flashlightPower = power;   
    }
}