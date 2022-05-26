using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float timeLeft;
    private Vector2 movement;
    public float accelerationTime = 2f;
    public float maxSpeed = 3f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -23.1f, 23.1f), Mathf.Clamp(transform.position.y, 17.5f, 40.5f), transform.position.z);
        timeLeft -= Time.deltaTime;


        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-23.1f, 23.1f), Random.Range(-17.5f, 40.5f));
            timeLeft += accelerationTime;
            
        }
    }

    private void FixedUpdate()
    {
        //for random movement
        //rb.AddForce(movement * maxSpeed);
        //level1
        //transform.Translate(movement); //level2
        BossLevel(PlayerPrefs.GetInt("BossLevel"));

    }

    void BossLevel(int i)
    {
        if(i == 0)
        {
            transform.Translate(movement * Time.deltaTime);
        }
        else if(i == 1)
        {
            transform.Translate(movement * Time.deltaTime);
        }
        else if (i == 2)
        {
            transform.Translate(movement * Time.deltaTime);
        }
        else if (i == 3)
        {
            transform.Translate(movement);
        }
        else if (i == 4)
        {
            transform.Translate(movement);
        }



    }
}
