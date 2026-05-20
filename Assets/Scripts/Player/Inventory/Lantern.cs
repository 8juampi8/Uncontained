using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : Item
{
    Light2D lux;

    GameObject player;
    GameObject LanternPosition;

    void Start()
    {
        lux = GameObject.FindWithTag("Light").GetComponent<Light2D>();

        player = GameObject.FindWithTag("Player");
        LanternPosition = GameObject.FindWithTag("LanternPos");
    }

    public override void Equip()
    {
        lux.enabled = true;

        transform.SetParent(LanternPosition.transform);
        transform.localPosition = Vector2.zero;
        transform.rotation = player.transform.rotation;
    }

    public override void Drop()
    {
        lux.enabled = false;

        transform.SetParent(null);
    }
}