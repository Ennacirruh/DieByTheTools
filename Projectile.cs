using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Vector3 target;
    public Vector2 velocity;
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector2 direction;
    [SerializeField] float lifespan = 5f;
    [SerializeField] string targetLayer;
    [SerializeField] bool farieFire;
    // Update is called once per frame
    private void Start()
    {
        direction = target - transform.position;
        direction = direction / direction.magnitude;
    }
    void FixedUpdate()
    {
        lifespan -= Time.fixedDeltaTime;
        if(lifespan < 0 )
        {
            Destroy(this.gameObject);
        }
        velocity = Vector2.Lerp(velocity, direction, Time.fixedDeltaTime * acceleration);
        GetComponent<Rigidbody2D>().position += velocity * speed * Time.fixedDeltaTime;
        GetComponent<Rigidbody2D>().rotation = transform.rotation.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            
            if (collision.gameObject.name == "Player")
            {
                collision.gameObject.GetComponent<Inventory>().Hit();
            }
            else
            {
                if(farieFire)
                {
                    collision.gameObject.GetComponent<EnemyHealthBar>().defenceShred= true;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
