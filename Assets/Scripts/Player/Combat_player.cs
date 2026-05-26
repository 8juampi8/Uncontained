using UnityEngine;

public class Combat_player : MonoBehaviour
{
    [SerializeField] private LayerMask enemies;

    private float meleRadius = 1.5f;
    private int meleDamage = 10;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Guns_gun gun = InvManager.Instance.CurrentGun;

            if (gun != null)
            {
                gun.Shoot();
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