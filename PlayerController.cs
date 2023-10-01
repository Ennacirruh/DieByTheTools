using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    Rigidbody2D rb;
    float velocityX;
    float velocityY;
    public float dashPower = 3f;
    public float dashCooldown;
    [SerializeField] float dashFallOff;
    float dashTimer;
    float velocityModifier = 1f;
    public bool blink = false;
    public bool dashImpact = false;
    [SerializeField] Weapon dashWeapon;
    [SerializeField] float dashImpactTimer;
    [SerializeField] bool queueImpact = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocityX = Input.GetAxisRaw("Horizontal");
        velocityY = Input.GetAxisRaw("Vertical");
        dashTimer -= Time.fixedDeltaTime;
        dashImpactTimer -= Time.fixedDeltaTime;
        velocityModifier = Mathf.MoveTowards(velocityModifier, 1f, dashFallOff * Time.fixedDeltaTime);
        if (Input.GetButton("Dash") && dashTimer <= 0)
        {
            dashTimer = dashCooldown;
            if (dashImpact)
            {
                dashImpactTimer = 0.15f;
                queueImpact= true;
            }
            if (blink)
            {
                Vector3 blinkDistance = new Vector2(velocityX, velocityY).normalized * velocityModifier * 3f;
                rb.position = transform.position + blinkDistance;
            }
            else
            {
                velocityModifier += dashPower;
            }
        }
        else
        {
            Vector3 velocity = new Vector2(velocityX, velocityY).normalized * Time.fixedDeltaTime * playerSpeed * velocityModifier;
            rb.MovePosition(transform.position + velocity);
        }
        if(queueImpact && dashImpactTimer <= 0)
        {
            queueImpact = false;
            dashWeapon.attack(gameObject.transform, null, false);
        }

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 direction = worldPos - (Vector2)transform.position;
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x)) - 90;
        rb.MoveRotation(Quaternion.Euler(new Vector3(0,0,angle)));
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

        }
    }
}
