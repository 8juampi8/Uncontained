using Unity.VisualScripting;
using UnityEngine;

public class Combat_player : MonoBehaviour
{
    [SerializeField] private LayerMask enemies;
    private float meleRadius = 1.5f;
    private int meleDamage = 10;
    PickGun_player gun;

    void Start()
    {
        gun = GetComponent<PickGun_player>();
    }

    void Update()
    {
        // SI ATACA, VE SI TIENE UN ARMA O NO
        if (Input.GetButtonDown("Fire1"))
        {
            // SI TIENE UN ARMA, SE FIJA QUE ARMA
            if (gun.equiped)
            {
                Guns_gun equippedGun = GameManager.instance.EquippedGun.GetComponent<Guns_gun>();

                if (equippedGun != null)
                {
                    equippedGun.Shoot();
                }
            }

            // SI NO TIENE ARMA, GOLPEA
            else
            {
                Collider2D meleHit = Physics2D.OverlapCircle(transform.position, meleRadius, enemies);

                if (meleHit != null)
                {
                    meleHit.gameObject.GetComponent<Health_enemy>().getDamage(meleDamage);
                }
            }
        }
    }

    // DIBUJAR EL CIRCLE EN LA ESCENA
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleRadius);
    }
}