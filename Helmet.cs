using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Helmet : ScriptableObject
{
    [SerializeField] int maxDefenceCharges;
    [SerializeField] int defenceCharges;
    [SerializeField] GameObject triggerEffect;
    public bool periodicTrigger;
    [SerializeField] float periodicTriggerTime;
    [SerializeField] float invincabilityTime;
    
    public void setHelm()
    {
        defenceCharges = maxDefenceCharges;
    }
    public bool trigger(Inventory inventory, bool damage = true)
    {
        if (damage)
        {
            defenceCharges--;
            inventory.triggerInvincability(invincabilityTime);
        }
        else
        {
            inventory.helmetTriggerTimer = periodicTriggerTime;
        }
        GameObject triggerObject = Instantiate(triggerEffect);
        triggerObject.transform.position = inventory.transform.position;
        triggerObject.GetComponent<HelmetTrigger>().trigger();
        if (defenceCharges > 0)
        {
            return false;
        }
        return true;
    }
}
