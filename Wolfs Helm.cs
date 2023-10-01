using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfsHelm : HelmetTrigger
{
    // Start is called before the first frame update
    [SerializeField] LayerMask enemies;
    [SerializeField] float radius;

    public override void trigger()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, enemies);
        if(hit != null)
        {
            Destroy(hit.gameObject);
        }
       
        

    }
}
