using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeyLantern : Spell
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileCount;
    [SerializeField] float projectileVelocityRange;
    [SerializeField] LayerMask targets;
    public override void trigger(GameObject player)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject pjtle = Instantiate(projectile);
            pjtle.transform.position = transform.position;
            Projectile pjtleComponent = pjtle.GetComponent<Projectile>();
            pjtleComponent.player = player;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 50, targets);
            pjtleComponent.target = hits[Random.Range(0,hits.Length)].gameObject.transform.position;
            pjtleComponent.velocity = new Vector2(Random.Range(-projectileVelocityRange, projectileVelocityRange), Random.Range(-projectileVelocityRange, projectileVelocityRange));
        }
        
    }
    // Start is called before the first frame update

}
