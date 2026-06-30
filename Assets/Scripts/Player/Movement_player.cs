using UnityEngine;

public class Movement_player : MonoBehaviour
{
    private float moveX, moveY;
    private float speed = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioSource footstepsSource;

    private Vector3 mousePos;

    private bool isMoving;

    void Start()
    {
        AudioManager.Instance.SetFootstepsSource(GetComponent<AudioSource>());
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        bool isMoving = Time.timeScale > 0 && moveX != 0 || moveY != 0;

        if (isMoving && !footstepsSource.isPlaying)
        {
            footstepsSource.loop = true;
            footstepsSource.Play();
        }
        else if (!isMoving && footstepsSource.isPlaying)
        {
            footstepsSource.Stop();
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = movement * speed;

        Vector2 aimDirection = mousePos - transform.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;

        rb.MoveRotation(aimAngle);
    }
}
