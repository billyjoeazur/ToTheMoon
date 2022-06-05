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
    private GameObject[] childList;
    
    void Awake()
    {
        childList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            childList[i] = transform.GetChild(i).gameObject;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CanShoot(15));
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

    IEnumerator CanShoot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        childList[0].gameObject.SetActive(true);
        childList[1].gameObject.SetActive(false);
        childList[2].gameObject.SetActive(false);
        childList[3].gameObject.SetActive(false);
        childList[4].gameObject.SetActive(false);
        childList[5].gameObject.SetActive(false);
        childList[6].gameObject.SetActive(true);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
