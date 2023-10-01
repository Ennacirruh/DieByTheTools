using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmoftheBulwark : HelmetTrigger
{
    [SerializeField] LayerMask projectiles;
    [SerializeField] float radius;

    public override void trigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, projectiles);
        for (int i = 0; i < hits.Length; i++)
        {
            Destroy(hits[i].gameObject);
        }
        
    }

}
