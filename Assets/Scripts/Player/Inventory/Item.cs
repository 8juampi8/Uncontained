using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    private Collider2D itemCollider;

    public abstract void Equip();

    public void Drop()
    {
        transform.SetParent(null);

        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = true;
    }
}