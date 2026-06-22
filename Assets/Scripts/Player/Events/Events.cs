using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Events : MonoBehaviour
{
    protected GameObject player;

    [SerializeField] protected GameObject typePanel;
    [SerializeField] protected Light2D globalLight;
    [SerializeField] protected Light2D freeLight;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            GameManager.Instance.SetSpawn(transform.position);
            Destroy(gameObject);
        }
    }
}