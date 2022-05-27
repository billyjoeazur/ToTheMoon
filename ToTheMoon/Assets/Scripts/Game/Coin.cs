using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    Rigidbody2D rb;
    float xForce;
    float yForce;
    Vector2 force;

    void Start()
    {

        //coins random drop/movement
        rb = GetComponent<Rigidbody2D>();
        xForce = Random.Range(-350f, 350f);
        yForce = Random.Range(350f, 450f);
        force = new Vector2(xForce, yForce);

        rb.AddForce(force);
    }


    void Update()
    {
        if (transform.position.y <= -70)
        {
            Destroy(gameObject);
        }
    }

}
