using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed = 1f;
    Transform player;
    Vector2 dir;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dir = (player.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);

        if (transform.position.y <= -70)
        {
            Destroy(gameObject);
        }
    }



    public void Init(float mySpeed, float scale)
    {
        
        speed = mySpeed;

        transform.localScale *= scale;

    }
}
