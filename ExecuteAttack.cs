using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ExecuteAttack : MonoBehaviour
{
    Vector2 startPos;
    Vector2 targetPos;
    float startRot;
    float targetRot;
    float currentRotation;
    float duration;
    float timer;
    bool begin = false;
    GameObject hand;
    List<GameObject> enemeisHit = new List<GameObject>();
    [SerializeField] Weapon weaponReference;

    private void FixedUpdate()
    {
        if (begin)
        {
            timer += Time.fixedDeltaTime / duration;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, timer);
            if (hand != null)
            {
                hand.transform.localPosition = transform.localPosition;
            }
            currentRotation = Mathf.Lerp(startRot, targetRot, timer);
            transform.localRotation = Quaternion.Euler(new Vector3(0,0,currentRotation));
            if(timer >= 1.1f)
            {
                if(this.transform.parent.name == "Origin")
                {
                    Destroy(this.transform.parent.gameObject);
                }
                if (hand != null)
                {
                    hand.transform.localPosition = Vector3.zero;
                }
                Destroy(this.gameObject);
            }
        }
    }

    public void attack(Vector2 startPos, Vector2 targetPos, float startRot, float targetRot, float time, GameObject hand= null)
    {
        this.startPos = startPos;
        this.startRot = startRot;
        this.duration = time;
        this.targetPos = targetPos;
        this.targetRot = targetRot;
        this.hand= hand;
        begin = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!enemeisHit.Contains(collision.gameObject))
        {
            enemeisHit.Add(collision.gameObject);
            collision.gameObject.GetComponent<EnemyHealthBar>().dealDamage(weaponReference.power, weaponReference.penetration);
            Vector2 direction = collision.gameObject.transform.position - collision.gameObject.GetComponent<EnemyHealthBar>().referenceAI.player.transform.position;
            direction = direction / direction.magnitude;
            collision.gameObject.GetComponent<EnemyHealthBar>().referenceAI.knockback += direction * weaponReference.knockback / 10f;
        }
    }
}
