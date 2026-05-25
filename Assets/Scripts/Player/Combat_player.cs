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
            if (InvManager.Instance.IsEquipped)
            {
                InvManager.Instance.Obj.GetComponent<Guns_gun>().Shoot();
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