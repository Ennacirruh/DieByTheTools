using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetTrigger : MonoBehaviour
{
    [SerializeField] float lifetime = 1;
    public virtual void trigger()
    {

    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
