using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBullet : MonoBehaviour
{
    float speed = 1;

    void Start()
    {
       
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y >= 70)
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
