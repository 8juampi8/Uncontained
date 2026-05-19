using UnityEngine;

public class Pickitem_player : MonoBehaviour
{

    private bool onKeyCard = false;
    private GameObject key;
    private bool hasKeyCard = false;
    public bool HasKeyCard => hasKeyCard;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (onKeyCard)
            {
                Destroy(key);
                hasKeyCard = true;
            }
        }
    }

    // SE FIJA SI ESTA ARRIBA DE ALGO
    void OnTriggerStay2D(Collider2D collision)
    {
        // SI ESTA ARRIBA DE UNA LLAVE
        if (collision.gameObject.CompareTag("KeyCard"))
        {
            onKeyCard = true;
            key = collision.gameObject;
        }
    }
}