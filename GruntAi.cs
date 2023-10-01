using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class GruntAi : AI
{
    [SerializeField] Enemy enemyReference;
    [SerializeField] Vector2 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = transform.position;
        GetComponent<EnemyHealthBar>().referenceAI = this;
        enemyReference = this.GetComponent<EnemyHealthBar>().enemyReference;
        cooldown = Random.Range(enemyReference.moveCooldownRange[0], enemyReference.moveCooldownRange[1]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldown -= Time.fixedDeltaTime;
        if(cooldown <= 0) 
        {
            cooldown = Random.Range(enemyReference.moveCooldownRange[0], enemyReference.moveCooldownRange[1]);
            Vector2 direction = player.transform.position - transform.position;
            direction = direction / direction.magnitude;

            targetPosition = (Vector2)transform.position + direction * enemyReference.mobility / 20f;
        }
        knockback = Vector2.Lerp(knockback, Vector2.zero, Time.fixedDeltaTime * enemyReference.maxHealth / 200f);
        GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, targetPosition + knockback, Time.fixedDeltaTime * enemyReference.mobility));
    }
}
