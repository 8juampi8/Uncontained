using UnityEngine;

public class FollowPlayer_enemy : MonoBehaviour
{
    private GameObject player;
    private int viewDistance = 15;
    private float speed = 2f;

    private SilenceHab_player silenceHab;

    [SerializeField] private LayerMask focusObjects;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        silenceHab = player.GetComponent<SilenceHab_player>();
    }

    void Update()
    {
        // SI SILENCE ESTA ACTIVO, NO LO SIGUE
        if (silenceHab.inSilence)
        {
            return;
        }

        // SI SILENCE NO ESTA ACTIVO Y EXISTE UN PLAYER, CALCULA DONDE ESTA
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;

            // SI ESTA DENTRO DE SU RANGO DE VISION, VERIFICA QUE NO HAYA UN OBSTACULO EN MEDIO
            if (direction.magnitude < viewDistance)
            {
                float distanceToPlayer = direction.magnitude;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distanceToPlayer, focusObjects);

                Debug.DrawRay(transform.position, direction.normalized * distanceToPlayer, Color.red);

                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

                    transform.rotation = Quaternion.Euler(0, 0, angle);
                    transform.Translate(0, 1 * speed * Time.deltaTime, 0);
                }
            }
        }
    }
}