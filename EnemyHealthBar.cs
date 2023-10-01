using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemyReference;
    [SerializeField] int health;
    public AI referenceAI;
    public bool defenceShred;
    private void Start()
    {
        health = enemyReference.maxHealth;
    }

    public void dealDamage(int damage, int pen)
    {
        if(defenceShred)
        {
            pen = 0;
        }
        health -= enemyReference.damageDealt(damage, pen);
        if(health <= 0)
        {
            if (enemyReference.immortal)
            {
                health = enemyReference.maxHealth;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
