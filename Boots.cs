using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Boots : ScriptableObject
{
    [SerializeField] BootEffects.BootType bootType;
    public void enable(GameObject player, bool inverse = false)
    {
        BootEffects.enable(player, bootType, inverse);
    }
}
