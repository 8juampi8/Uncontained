using UnityEngine;

public class SilenceHab_player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;

    private int silenceCooldown = 20;
    private float silenceWait = 20;
    private bool canSilence = false;
    private int silenceDuration = 5;
    private float silenceTimer = 0;
    private bool silence = false;

    public bool inSilence => silence;

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
        if (Input.GetKeyDown(KeyCode.X) && canSilence)
        {
            silence = true;
        }

        // MIENTRAS SILENCE ESTE ACTIVO
        if (silence)
        {
            playerSprite.color = Color.gray4;

            silenceTimer += Time.deltaTime;

            silenceWait = 0;

            if (silenceTimer >= silenceDuration)
            {
                silence = false;
                silenceTimer = 0;
            }
        }
        else
        {
            silenceWait += Time.deltaTime;

            playerSprite.color = Color.white;
        }
    }
}