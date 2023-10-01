using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathsHelm : HelmetTrigger
{
    [SerializeField] LayerMask targets;
    [SerializeField] float radius;

    public override void trigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, targets);
        for (int i = 0; i < hits.Length; i++)
        {
            Destroy(hits[i].gameObject);
        }

    }
}
