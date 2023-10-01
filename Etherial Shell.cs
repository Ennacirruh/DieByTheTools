using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherialShell : Spell
{
    [SerializeField] GameObject barrier;
    [SerializeField] float summonDistance;

    public override void trigger(GameObject player)
    {
        Vector2 direction = player.transform.up * summonDistance;
        GameObject barrierInstance = Instantiate(barrier);
        barrierInstance.transform.position = (Vector2)player.transform.position + direction;
        barrierInstance.transform.rotation = player.transform.rotation;
    }
}
