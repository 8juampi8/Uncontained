using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    private Collider2D itemCollider;

    public virtual void Equip()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = false;   
    }

    public virtual void Drop()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = true;
    }
}