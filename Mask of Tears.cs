using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MaskofTears : Spell
{
    [SerializeField] GameObject areaOfEffect;
    [SerializeField] float summonDistance;
    public override void trigger(GameObject player)
    {
        Vector2 direction = player.transform.up * summonDistance;
        GameObject barrierInstance = Instantiate(areaOfEffect);
        barrierInstance.transform.position = (Vector2)player.transform.position + direction;
        barrierInstance.transform.rotation = player.transform.rotation;
    }
}
