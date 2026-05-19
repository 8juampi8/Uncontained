using UnityEngine;

public class Movement_player : MonoBehaviour
{
    private float moveX, moveY;
    private float speed = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;

    private Vector3 mousePos;

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
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
