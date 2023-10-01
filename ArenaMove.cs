using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaMove : MonoBehaviour
{
    [SerializeField] Vector2 defaultPosition;
    [SerializeField] Vector2 defaultScale;
    [SerializeField] Vector2 targetPosition;
    [SerializeField] Vector2 targetScale;
    [SerializeField] float moveSpeed;
    [SerializeField] float scaleSpeed;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultPosition= rb.position;
        defaultScale= rb.transform.localScale;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position,targetPosition,Time.fixedDeltaTime * moveSpeed));
        transform.localScale = Vector2.MoveTowards((Vector2)transform.localScale,targetScale,Time.fixedDeltaTime * scaleSpeed);
    }

    public void ResetObsticle(float speed)
    {
        moveSpeed = speed;
        scaleSpeed= speed;
        targetPosition= defaultPosition;
        targetScale= defaultScale;
    }

    public void move(Vector2 newPosition, float speed)
    {
        moveSpeed= speed;
        targetPosition = newPosition;
    }

    public void setScale(Vector2 newScale, float speed)
    {
        scaleSpeed= speed;
        targetScale= newScale;
    }
}
