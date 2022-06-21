using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPenetrate : MonoBehaviour
{
    public int penetrateNumber = 2;

    void Update()
    {
        if(penetrateNumber == 0)
		{
			Destroy(gameObject);
		}
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy" || other.tag == "Boss")
		{
			penetrateNumber -= 1;
		}
	}
}
