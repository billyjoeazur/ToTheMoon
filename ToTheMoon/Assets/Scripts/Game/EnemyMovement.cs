using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float timeLeft;
    private Vector2 movement;
    public float accelerationTime;
    public float maxSpeed;
    Rigidbody2D rb;
	public float downSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }

        transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
        Destroy(gameObject, 7f);

    }

    private void FixedUpdate()
    {
        //for random movement
        rb.AddForce(movement * maxSpeed);
    }
}
