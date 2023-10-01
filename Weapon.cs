using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Weapon: ScriptableObject
{
    public int power;
    public int penetration;
    public float knockback;
    [SerializeField] bool createProjectile;
    [SerializeField] bool playerLocked;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject weapon;
    [SerializeField] int burst = 1;
    [SerializeField] Vector2[] positionSequence;
    [SerializeField] Vector2[] positionFlollowThroughSequence;
    [SerializeField] float[] rotationSequence;
    [SerializeField] float[] rotationFlollowThroughSequence;
    public int seqeunceStep;
    [SerializeField] float[] sequenceStepTime;
    public float[] sequenceStepCooldown;

    public void attack(Transform parent, GameObject hand, bool invert)
    {
        float multiplier = 1f;
        if (invert)
        {
            multiplier *= -1;
        }
        Vector2 vectorMultiplier = new Vector2(multiplier, 1f);
        for (int i = 0; i < burst; i++)
        {
            GameObject weaponInstance = Instantiate(weapon);
            if (playerLocked)
            {
                weaponInstance.transform.SetParent(parent);
            }
            else
            {
                GameObject origin = new GameObject("Origin");
                origin.transform.position = parent.transform.position;
                origin.transform.rotation = parent.transform.rotation;
                weaponInstance.transform.SetParent(origin.transform);
                hand = null;
            }
            
            weaponInstance.transform.localPosition = positionSequence[seqeunceStep] * multiplier;
            weaponInstance.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotationSequence[seqeunceStep] * multiplier));
            weaponInstance.GetComponent<ExecuteAttack>().attack(positionSequence[seqeunceStep] * vectorMultiplier, positionFlollowThroughSequence[seqeunceStep] * vectorMultiplier, rotationSequence[seqeunceStep] * multiplier, rotationFlollowThroughSequence[seqeunceStep] * multiplier, sequenceStepTime[seqeunceStep], hand);
            seqeunceStep += 1;
            if (seqeunceStep == positionSequence.Length)
            {
                seqeunceStep = 0;
            }
        }
    }
}
