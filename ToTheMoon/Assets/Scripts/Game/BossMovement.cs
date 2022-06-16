using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float timeLeft;
    private Vector2 movement;
    public float accelerationTime;
    public float maxSpeed;
    Rigidbody2D rb;
    
    void Awake()
    {
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CanShoot(5));
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
        transform.Translate(movement * Time.deltaTime);
    }

    IEnumerator CanShoot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
