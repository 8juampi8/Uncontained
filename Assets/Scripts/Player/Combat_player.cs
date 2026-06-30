using UnityEngine;
using System.Collections;

public class Combat_player : MonoBehaviour
{
    [SerializeField] private LayerMask enemies;

    private float meleRadius = 1.5f;
    private int meleDamage = 10;

    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject reloadTxt;

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
                }
                else 
                {
                    StartCoroutine(HaveToReload());
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
                    meleHit.GetComponent<Health_enemy>().getDamage(meleDamage);
                }
            }
        }
    }

    IEnumerator HaveToReload()
    {
        float dly = 0.5f;
        reloadTxt.SetActive(true);
        yield return new WaitForSeconds(dly);
        reloadTxt.SetActive(false);
    }
}