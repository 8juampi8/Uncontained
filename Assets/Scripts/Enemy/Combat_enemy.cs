using UnityEngine;

public class Combat_enemy : MonoBehaviour
{
    private int damage = 1;

    private float hitTimer = 0;
    private float hitCooldown = 1.5f;

    [SerializeField] private Animator anim;

    void Update()
    {
        hitTimer += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hitTimer >= hitCooldown)
        {
            anim.SetBool("isHitting", true);
            GameManager.instance.getDamage(damage);
            hitTimer = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hitTimer >= hitCooldown)
        {
            anim.SetBool("isHitting", false);
        }
    }
}