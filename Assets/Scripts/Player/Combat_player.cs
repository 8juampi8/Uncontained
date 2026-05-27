using UnityEngine;

public class Combat_player : MonoBehaviour
{
    [SerializeField] private LayerMask enemies;

    private float meleRadius = 1.5f;
    private int meleDamage = 10;

    void Update()
    {
        if (InvManager.Instance.CurrentGun != null) InvManager.Instance.CurrentGun.UpdateShootTimer();

        if (Input.GetButtonDown("Fire1"))
        {
            if (InvManager.Instance.IsEquipped)
            {
                if (InvManager.Instance.CurrentGun.GunCharger > 0)
                {
                    InvManager.Instance.CurrentGun.Shoot();
                }
                else
                {
                    Debug.Log("No tenes balas");
                }
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