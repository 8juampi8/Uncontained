using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    Light2D lght;

    [SerializeField] private float power = 100;

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

    void Update()
    {
        FlashlightLife();
    }

    private void FlashlightLife()
    {
        if (power <= 0)
        {
            lght.enabled = false;
            power = 0;
        }

        if (lght.enabled && power > 0)
        {
            power -= Time.deltaTime;
        }
    }

    public void AddPower()
    {
        power += 25;
    }
}