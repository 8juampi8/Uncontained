using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SilenceHab_player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private AudioClip silenceSound;

    private int silenceCooldown = 15;
    private float silenceWait = 15;
    private bool canSilence = false;
    private int silenceDuration = 5;
    private float silenceTimer = 0;
    private bool silence = false;

    public bool inSilence => silence;

    private Image greenCircle;

    void Start()
    {
        greenCircle = GameObject.FindWithTag("GreenCircle").GetComponent<Image>();
    }

    void Update()
    {
        //Veo que no este en cooldown
        if (silenceWait >= silenceCooldown)
        {
            canSilence = true;
        }
        else
        {
            canSilence = false;
        }

        bool puedeActivar = false;

        if (dialogue != null)
        {
            if (!dialogue.activeSelf)
            {
                puedeActivar = true;
            }
        }
        else
        {
            puedeActivar = true;
        }

        //Activacion de habilidad
        if (Input.GetKeyDown(KeyCode.X) && canSilence && puedeActivar)
        {
            silence = true;
            if (AudioManager.Instance != null && silenceSound != null)
            {
                AudioManager.Instance.PlaySFX(silenceSound);
            }

            silenceWait = 0;
        }

        //Efecto de silence
        if (silence)
        {
            playerSprite.color = Color.gray;
            silenceTimer += Time.deltaTime;

            if (silenceTimer >= silenceDuration)
            {
                if (AudioManager.Instance != null && silenceSound != null)
                {
                    AudioManager.Instance.PlaySFX(silenceSound);
                }

                silence = false;
                silenceTimer = 0;
            }
        }
        else
        {
            playerSprite.color = Color.white;
        }

        silenceWait += Time.deltaTime;
        float alpha = Mathf.Clamp01(silenceWait / silenceCooldown);

        if (greenCircle != null)
        {
            greenCircle.fillAmount = alpha;
        }
    }
    public void ResetWait()
    {
        silenceWait = 10;
    }
}