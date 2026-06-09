using UnityEngine;

public class FollowPlayer_enemy : MonoBehaviour
{
    private GameObject player;
    private int viewDistance;
    private float speed = 2f;

    private SilenceHab_player silenceHab;

    [SerializeField] private LayerMask focusObjects;

    [SerializeField] private SpriteRenderer eyeSprite;

    private bool wasFollowing = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        silenceHab = player.GetComponent<SilenceHab_player>();
    }

    void Update()
    {
        if (silenceHab.inSilence)
        {
            viewDistance = 3;
        }
        else
        {
            viewDistance = 10;
        }

        bool currentlyFollowing = false;

        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;

            if (direction.magnitude < viewDistance)
            {
                float distanceToPlayer = direction.magnitude;

                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position,
                    direction.normalized,
                    distanceToPlayer,
                    focusObjects
                );

                Debug.DrawRay(
                    transform.position,
                    direction.normalized * distanceToPlayer,
                    Color.red
                );

                if (hit.collider != null &&
                    hit.collider.CompareTag("Player"))
                {
                    currentlyFollowing = true;

                    float angle =
                        Mathf.Atan2(direction.y, direction.x) *
                        Mathf.Rad2Deg - 90;

                    transform.rotation =
                        Quaternion.Euler(0, 0, angle);

                    transform.Translate(
                        0,
                        speed * Time.deltaTime,
                        0
                    );
                }
            }
        }

        if (currentlyFollowing)
        {
            eyeSprite.enabled = true;
        }
        else
        {
            eyeSprite.enabled = false;
        }

        if (currentlyFollowing && !wasFollowing)
        {
            GameManager.Instance.OnFollowing();
        }

        if (!currentlyFollowing && wasFollowing)
        {
            GameManager.Instance.OffFollowing();
        }

        wasFollowing = currentlyFollowing;
    }
}