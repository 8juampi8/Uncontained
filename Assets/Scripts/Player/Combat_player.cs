using UnityEngine;

public class Combat_player : MonoBehaviour
{
    [SerializeField] private LayerMask enemies;

    private float meleRadius = 1.5f;
    private int meleDamage = 10;

    private ItemInteraction_player item;

    void Start()
    {
        item = GetComponent<ItemInteraction_player>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (item.EquippedGun != null)
            {
                item.EquippedGun.Shoot();
            }
            else
            {
                Collider2D meleHit =
                    Physics2D.OverlapCircle(
                        transform.position,
                        meleRadius,
                        enemies);

                if (meleHit != null)
                {
                    meleHit
                        .GetComponent<Health_enemy>()
                        .getDamage(meleDamage);
                }
            }
        }
    }
}