using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScatter : MonoBehaviour
{
    Rigidbody2D rb;
    float xForce;
    float yForce;
    Vector2 force;

    void Start()
    {
        //ricochet
        rb = GetComponent<Rigidbody2D>();
        xForce = Random.Range(-5000, 5000);
        yForce = Random.Range(2000f, 2000f);
        force = new Vector2(xForce, yForce);

        rb.AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AddTag(0.1f));
        Destroy(gameObject, 1.5f);
    }
    
    IEnumerator AddTag(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.tag = "Bullet";
    }
}
