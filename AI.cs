using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Vector2 knockback;
    public GameObject player;
    public float cooldown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Inventory>().Hit();
        }
    }
    public void modifyCooldown(float modifier)
    {
        cooldown = 0;
        cooldown += modifier;
    }
}
