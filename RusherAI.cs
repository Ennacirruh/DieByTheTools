using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RusherAI : AI
{
    [SerializeField] Enemy enemyReference;
    [SerializeField] Vector2 targetPosition;
    [SerializeField] float rotationSpeed;
    [SerializeField] float retreatThreashold;
    [SerializeField] float jinkThreashold;
    [SerializeField] Vector2 chargeRange;
    [SerializeField] float charge;
    [SerializeField] float chargeThreashold;
    [SerializeField] float chargeDuration;
    [SerializeField] float releaseTime;
    [SerializeField] float chargeTimer;
    [SerializeField] Vector2 chargeVector;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = transform.position;
        GetComponent<EnemyHealthBar>().referenceAI = this;
        enemyReference = this.GetComponent<EnemyHealthBar>().enemyReference;
        cooldown = Random.Range(enemyReference.moveCooldownRange[0], enemyReference.moveCooldownRange[1]);;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (charge < chargeThreashold)
        {
            cooldown -= Time.fixedDeltaTime;
            chargeTimer = chargeDuration;
            if (cooldown <= 0)
            {
                cooldown = Random.Range(enemyReference.moveCooldownRange[0], enemyReference.moveCooldownRange[1]);
                charge += Random.Range(chargeRange[0], chargeRange[1]);
                Vector2 direction = player.transform.position - transform.position;
                if (direction.magnitude < retreatThreashold)
                {
                    direction = -direction / direction.magnitude;

                }
                else if (direction.magnitude < jinkThreashold)
                {
                    direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    direction = direction / direction.magnitude;

                }
                else
                {
                    direction = direction / direction.magnitude;
                }


                targetPosition = (Vector2)transform.position + direction * enemyReference.mobility / 20f;
            }
            knockback = Vector2.Lerp(knockback, Vector2.zero, Time.fixedDeltaTime * enemyReference.maxHealth / 200f);
            GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, targetPosition + knockback, Time.fixedDeltaTime * enemyReference.mobility));
            GetComponent<Rigidbody2D>().MoveRotation(Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime)));
        }
        else
        {
            chargeTimer -= Time.fixedDeltaTime;
            if (chargeDuration - chargeTimer >= releaseTime)
            {
                targetPosition = (Vector2)transform.position + chargeVector * enemyReference.mobility / 40f;
                GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * enemyReference.mobility));
            }
            else
            {
                Vector2 direction = player.transform.position - transform.position;
                direction = direction / direction.magnitude;
                chargeVector = direction;
            }
            GetComponent<Rigidbody2D>().MoveRotation(Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + rotationSpeed * 4f * Time.fixedDeltaTime)));
            if(chargeTimer <= 0)
            {
                charge = 0;
            }
        }
    }
}
