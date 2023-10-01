using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BootEffects
{
    public enum BootType
    {
        Assasin,
        Messanger,
        Tyrant,
        Mage,
        Traveler
    }
    public static void enable(GameObject player, BootType bootType, bool inverse = false)
    {
        float modifier = 1f;
        if(inverse)
        {
            modifier = -1f;
        }
        switch(bootType)
        {
            case BootType.Assasin:
                player.GetComponent<PlayerController>().dashPower += 1f * modifier; 
                player.GetComponent<PlayerController>().dashCooldown -= 0.5f * modifier;
                break;
            case BootType.Messanger:
                player.GetComponent<PlayerController>().playerSpeed += 1f * modifier;
                break;
            case BootType.Tyrant:
                player.GetComponent<PlayerController>().dashPower -= 0.5f * modifier;
                player.GetComponent<PlayerController>().dashCooldown += 0.5f * modifier;
                if (inverse)
                {
                    player.GetComponent<PlayerController>().dashImpact = false;
                }
                else
                {
                    player.GetComponent<PlayerController>().dashImpact = true;
                }
                break;
            case BootType.Mage:
                if (inverse)
                {
                    player.GetComponent<PlayerController>().blink = false;
                }
                else
                {
                    player.GetComponent<PlayerController>().blink = true;
                }
                break;
            case BootType.Traveler:
                player.GetComponent<Inventory>().attackSpeedModifier -= 0.1f * modifier;
                break;
            default:
                Debug.LogError("Uhh Ohh, this boot aint gat any exist, kk");
                break;
        }
    }
}
