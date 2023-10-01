using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmoftheGateCrasher : HelmetTrigger
{
    [SerializeField] Weapon gateCrasher;
    
    public override void trigger()
    {
        gateCrasher.attack(gameObject.transform, null, false);
    }
    
}
