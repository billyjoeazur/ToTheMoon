using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField, Tooltip("How much the enemy's health decrease.")]
    private int bulletDamage = 50;
    private int bulletKnockbackDamage = 50;
    private int bulletSlowDamage = 50;
    private int bulletPenetrateDamage = 50;
    private int bulletScatterDamage = 50;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Bullet"))
        {
            this.gameObject.GetComponent<EnemyProfile>().DecreaseHealth(bulletDamage);
            Destroy(other.gameObject);  //destroy bullet
        }
        
        if (other.gameObject.tag == "BulletKnockback")
        {
            this.gameObject.GetComponent<EnemyProfile>().DecreaseHealth(bulletKnockbackDamage);
            transform.Translate(Vector2.up * 400 * Time.deltaTime);
            Destroy(other.gameObject);  //destroy bullet
        }
        
        if (other.gameObject.tag == "BulletSlow")
		{
			this.gameObject.GetComponent<EnemyProfile>().DecreaseHealth(bulletSlowDamage);
            if (gameObject.tag == "Enemy")
                {
                    //slow down the enemy movement
                    this.GetComponent<EnemyMovement>().maxSpeed = 2.5f;
                    this.GetComponent<EnemyMovement>().downSpeed = 5f;
                }
				//this.GetComponent<SpriteRenderer>().color = Color.cyan;
				Destroy(other.gameObject);  //destroy bullet
            
		}
        
        if (other.CompareTag("BulletPenetrate"))
        {
            this.gameObject.GetComponent<EnemyProfile>().DecreaseHealth(bulletPenetrateDamage);
        }
        
        if (other.CompareTag("BulletScatter"))
        {
            int scatterCount = 3;
            this.gameObject.GetComponent<EnemyProfile>().DecreaseHealth(bulletScatterDamage);
            if (gameObject.tag == "Enemy")
            {
                // while (scatterCount > 0)
                // {
                //     BulletScatter scatter = Instantiate(scatterPrefab, transform.position, Quaternion.identity);
                //     scatterCount--;
                // }
            }
            
        }
        
    }
}
