using UnityEngine;

public class Combat_player : MonoBehaviour
{
    [SerializeField] private LayerMask enemies;

    private float meleRadius = 1.5f;
    private int meleDamage = 10;

    [SerializeField] private GameObject[] panels;

    void Update()
    {
        if (InvManager.Instance.CurrentGun != null)
            InvManager.Instance.CurrentGun.UpdateShootTimer();

        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (panels[i].activeSelf) return;
            }

            if (InvManager.Instance.IsEquipped)
            {
                if (InvManager.Instance.CurrentGun.GunCharger > 0)
                {
                    InvManager.Instance.TriggerShootAnimation();

                    InvManager.Instance.CurrentGun.Shoot();
                    AudioManager.Instance.PlaySFX(InvManager.Instance.ShootSound);
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