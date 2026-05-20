using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    protected Collider2D itemCollider;

    public virtual void Equip()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = false;   
    }
}