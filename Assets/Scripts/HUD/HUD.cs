using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image healthImg;
    [SerializeField] private TextMeshProUGUI bulletsTxt;
    [SerializeField] private TextMeshProUGUI moreBulletsTxt;
    [SerializeField] private Slider flashlightPower;
    public Slider FlashlightPower => flashlightPower;

    void Start()
    {
        GameManager.Instance.SaveHealth(healthImg);
        GameManager.Instance.SaveAmmo(bulletsTxt);
        GameManager.Instance.SaveMoreAmmo(moreBulletsTxt);
        GameManager.Instance.SaveFLpower(flashlightPower);
    }
}