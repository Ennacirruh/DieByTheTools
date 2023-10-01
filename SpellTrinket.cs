using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SpellTrinket : ScriptableObject
{
    public float coodown;
    [SerializeField] GameObject spell;

    public void trigger(GameObject player)
    {
        GameObject spellInstance = Instantiate(spell);
        spellInstance.transform.position = player.transform.position;
        spellInstance.GetComponent<Spell>().trigger(player);
    }
}
