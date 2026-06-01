using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    private float power;
    Light2D lght;

    private GameObject player;
    private PlayerSoundsController soundsController;

    void Start()
    {
        lght = GetComponent<Light2D>();

        power = GameManager.Instance.FlashlightPower;

        player = GameObject.FindWithTag("Player");

        if (player != null) soundsController = player.GetComponent<PlayerSoundsController>();
    }

    public void Toggle()
    {
        if (Time.timeScale > 0)
        {
            if (lght != null)
            {    
                lght.enabled = !lght.enabled;

                soundsController.PlayToggleFL();
            }
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

        if (power > 100)
        {
            power = 100;
        }

        if (lght.enabled && power > 0)
        {
            power -= Time.deltaTime;
        }
        GameManager.Instance.ChangePower(power);
        GameManager.Instance.UpdateFLpower();
    }

    public void AddPower()
    {
        power += 25;
        GameManager.Instance.ChangePower(power);
    }
}