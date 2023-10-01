using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpshooterAI : AI
{
    [SerializeField] Enemy enemyReference;
    [SerializeField] Vector2 targetPosition;
    [SerializeField] float rotationSpeed;
    [SerializeField] float retreatThreashold;
    [SerializeField] float straifThreashold;
    [SerializeField] float straifDirectionThreashold;
    [SerializeField] GameObject projectile;
    [SerializeField] Vector2Int projectileCount;
    [SerializeField] float projectileVelocityRange;
    [SerializeField] float projectileCooldown;
    [SerializeField] float projectileTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = transform.position;
        GetComponent<EnemyHealthBar>().referenceAI = this;
        enemyReference = this.GetComponent<EnemyHealthBar>().enemyReference;
        cooldown = Random.Range(enemyReference.moveCooldownRange[0], enemyReference.moveCooldownRange[1]);
        straifDirectionThreashold = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldown -= Time.fixedDeltaTime;
        projectileTimer -= Time.fixedDeltaTime;
        if (cooldown <= 0)
        {
            cooldown = Random.Range(enemyReference.moveCooldownRange[0], enemyReference.moveCooldownRange[1]);
            Vector2 direction = player.transform.position - transform.position;
            if(direction.magnitude < retreatThreashold)
            {
                direction = -direction / direction.magnitude;
                
            }
            else if (direction.magnitude < straifThreashold)
            {
                if(Random.Range(0,1f) < straifDirectionThreashold)
                {
                    direction = new Vector2(direction.y, -1f * direction.x) / direction.magnitude;
                }
                else
                {
                    direction = -1f * new Vector2(direction.y, -1f * direction.x) / direction.magnitude;
                }
                
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
        if(projectileTimer <= 0)
        {
            for (int i = 0; i < Random.Range(projectileCount[0], projectileCount[1]); i++)
            {
                projectileTimer = projectileCooldown;
                GameObject pjtle = Instantiate(projectile);
                pjtle.transform.position = transform.position;
                Projectile pjtleComponent = pjtle.GetComponent<Projectile>();
                pjtleComponent.player = player;
                pjtleComponent.target = player.transform.position;
                pjtleComponent.velocity = new Vector2(Random.Range(-projectileVelocityRange, projectileVelocityRange), Random.Range(-projectileVelocityRange, projectileVelocityRange));
            }
        }
    }
}
