using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed;

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -26.1f, 26.1f), Mathf.Clamp(transform.position.y, -46, 36), transform.position.z);
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 TouchPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(TouchPosition.x * movementSpeed * Time.deltaTime, TouchPosition.y * movementSpeed * Time.deltaTime, 0);
        }
    }
}
