using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] GameObject leftHandVisual;
    [SerializeField] GameObject rightHandVisual;
    [SerializeField] bool helmetEnabled = true;
    [SerializeField] public Helmet helmet;
    [SerializeField] bool leftWeaponEnabled = true;
    [SerializeField] public Weapon leftWeapon;
    [SerializeField] bool rightWeaponEnabled = true;
    [SerializeField] public Weapon rightWeapon;
    [SerializeField] bool trinketOneEnabled = true;
    [SerializeField] public SpellTrinket trinketOne;
    [SerializeField] bool trinketTwoEnabled = true;
    [SerializeField] public SpellTrinket trinketTwo;
    [SerializeField] bool bootsEnabled = true;
    [SerializeField] public Boots boots;
    [SerializeField] List<string> equipmentDestuctionReference= new List<string>();
    [SerializeField] float invincabilityTimer;
    public float helmetTriggerTimer;
    public float attackSpeedModifier = 1f;
    float leftTimer;
    float rightTimer;
    [SerializeField] float tOneTimer;
    float tTwoTimer;
    void Start()
    {
        equipmentDestuctionReference.Add("Boots");
        equipmentDestuctionReference.Add("Left Weapon");
        equipmentDestuctionReference.Add("Right Weapon");
        equipmentDestuctionReference.Add("Trinket 1");
        equipmentDestuctionReference.Add("Trinket 2");

        helmet.setHelm();
        boots.enable(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        invincabilityTimer -= Time.deltaTime;
        leftTimer -= Time.deltaTime * attackSpeedModifier;
        rightTimer -= Time.deltaTime * attackSpeedModifier;
        tOneTimer -= Time.deltaTime;
        tTwoTimer -= Time.deltaTime;
        helmetTriggerTimer -= Time.deltaTime;
        if (helmet.periodicTrigger && helmetTriggerTimer <= 0 && helmetEnabled)
        {
            helmet.trigger(this, false);
        }
        if (Input.GetButton("Left Weapon") && leftTimer <= 0 && leftWeaponEnabled)
        {
            leftTimer = leftWeapon.sequenceStepCooldown[leftWeapon.seqeunceStep];
            leftWeapon.attack(leftHand.transform, leftHandVisual, false);
        }
        if (Input.GetButton("Right Weapon") && rightTimer <= 0 && rightWeaponEnabled)
        {
            rightTimer = rightWeapon.sequenceStepCooldown[rightWeapon.seqeunceStep];
            rightWeapon.attack(rightHand.transform, rightHandVisual, true);
        }
        if(Input.GetButton("Trinket One") && tOneTimer <= 0 && trinketOneEnabled)
        {
            tOneTimer = trinketOne.coodown;
            trinketOne.trigger(this.gameObject);
        }
        if (Input.GetButton("Trinket Two") && tTwoTimer <= 0 && trinketTwoEnabled)
        {
            trinketTwo.trigger(this.gameObject);
            tTwoTimer = trinketTwo.coodown;
        }
    }

    public void Hit()
    {
        if (invincabilityTimer <= 0)
        {
            if (helmetEnabled)
            {
                if (helmet.trigger(this))
                {
                    helmetEnabled = false;
                }
            }
            else
            {
                invincabilityTimer = 1.5f;
                switch (equipmentDestuctionReference[Random.Range(0, equipmentDestuctionReference.Count)])
                {
                    case "Boots":
                        bootsEnabled = false;
                        boots.enable(gameObject, true);
                        equipmentDestuctionReference.Remove("Boots");
                        break;
                    case "Left Weapon":
                        leftWeaponEnabled = false;
                        equipmentDestuctionReference.Remove("Left Weapon");
                        break;
                    case "Right Weapon":
                        rightWeaponEnabled = false;
                        equipmentDestuctionReference.Remove("Right Weapon");
                        break;
                    case "Trinket 1":
                        trinketOneEnabled = false;
                        equipmentDestuctionReference.Remove("Trinket 1");
                        break;
                    case "Trinket 2":
                        trinketTwoEnabled = false;
                        equipmentDestuctionReference.Remove("Trinket 2");
                        break;
                    default:
                        Debug.LogError("Hey Idiot, something wasnt destroyed properly!");
                        break;

                }
            }



            if (helmetEnabled == false && leftWeaponEnabled == false && rightWeaponEnabled == false && bootsEnabled == false && trinketOneEnabled == false && trinketTwoEnabled == false)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("GameController");
        manager.GetComponent<UIControls>().Die();
    }

    public void triggerInvincability(float duration)
    {
        invincabilityTimer = duration;
    }
}
