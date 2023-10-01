using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField] float lifetime = 1;
    [SerializeField] string target;
    [SerializeField] float freezeDuration;
    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer(target))
        {
            collision.gameObject.GetComponent<EnemyHealthBar>().referenceAI.modifyCooldown(freezeDuration);
        }
    }
}
