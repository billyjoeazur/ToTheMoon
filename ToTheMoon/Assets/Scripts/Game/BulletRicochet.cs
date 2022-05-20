using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRicochet : MonoBehaviour
{
    Rigidbody2D rb;
    float xForce;
    float yForce;
    Vector2 force;


    void Start()
    {
        //ricochet
        rb = GetComponent<Rigidbody2D>();
        xForce = Random.Range(-350f, 350f);
        yForce = Random.Range(350f, 450f);
        force = new Vector2(xForce, yForce);

        rb.AddForce(force);
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1f);
    }
}
