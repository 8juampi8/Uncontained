using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SilenceHab_player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;

    private int silenceCooldown = 15;
    private float silenceWait = 15;
    private bool canSilence = false;
    private int silenceDuration = 5;
    private float silenceTimer = 0;
    private bool silence = false;

    public bool inSilence => silence;

    private PlayerSoundsController soundsController;

    [SerializeField] private GameObject dialogue;

    private Image greenCircle;

    void Start()
    {
        soundsController = gameObject.GetComponent<PlayerSoundsController>();
        greenCircle = GameObject.FindWithTag("GreenCircle").GetComponent<Image>();
    }

    void Update()
    {
        // VERIFICAR QUE SILENCE NO ESTE EN COOLDOWN
        if (silenceWait >= silenceCooldown)
        {
            canSilence = true;
        }
        else
        {
            canSilence = false;
        }

        // SI APRETA X Y SILENCE NO ESTA EN COOLDOWN, SE ACTIVA
        if (dialogue != null)
        {
            if (Input.GetKeyDown(KeyCode.X) && canSilence && !dialogue.activeSelf)
            {
                silence = true;
                soundsController.PlaySilence();
                silenceWait = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X) && canSilence)
            {
                silence = true;
                soundsController.PlaySilence();
                silenceWait = 0;
            }
        }

        // MIENTRAS SILENCE ESTE ACTIVO
        if (silence)
        {

            playerSprite.color = Color.gray4;

            silenceTimer += Time.deltaTime;

            if (silenceTimer >= silenceDuration)
            {
                soundsController.PlaySilence();

                silence = false;
                silenceTimer = 0;
            }
        }
        else
        {
            playerSprite.color = Color.white;
        }

        silenceWait += Time.deltaTime;

        float alpha = silenceWait / silenceCooldown;
        alpha = Mathf.Clamp01(alpha);

        if(greenCircle == null)
        {
            greenCircle = GameObject.FindWithTag("GreenCircle").GetComponent<Image>();
        }
        else
        {            
            greenCircle.fillAmount = alpha;
        }
    }

    public void ResetWait()
    {
        silenceWait = 10;
    }
}