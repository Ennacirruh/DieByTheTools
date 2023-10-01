using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DragonTooth : Spell
{
    [SerializeField] float modifier;
    [SerializeField] GameObject playerMemory;
    public override void trigger(GameObject player)
    {
        playerMemory = player;
        player.GetComponent<Inventory>().attackSpeedModifier += modifier;
    }
    private void OnDestroy()
    {
        if (playerMemory != null)
        {
            playerMemory.GetComponent<Inventory>().attackSpeedModifier -= modifier;
        }
    }
}
