using System;
using UnityEngine;

public class Create_bullet : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float destroyTime;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(0, 1 * speed * Time.deltaTime, 0);
    }
}