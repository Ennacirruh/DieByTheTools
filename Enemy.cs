using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public int maxHealth;
    [SerializeField] int defence;
    public int mobility;
    public Vector2 moveCooldownRange;
    public bool immortal;

    public int damageDealt(int damage, int penetration)
    {
        return (int)Mathf.Clamp(damage - 2f * (defence - penetration * 1.25f), 0.5f * penetration + 1, Mathf.Infinity);
    }

}
