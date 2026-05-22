using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;


    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;
}
