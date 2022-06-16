using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyMovement : MonoBehaviour
{
    public float downSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
    }
}
