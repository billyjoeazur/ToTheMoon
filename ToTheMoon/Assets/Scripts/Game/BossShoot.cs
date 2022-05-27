using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{

    [SerializeField]
    Transform spawnspot;

    [SerializeField]
    EnemyBullet bulletPrefab;

    Transform player;
    [SerializeField]
    float shootInterval;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("ShootPlayer", shootInterval, shootInterval);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void ShootPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        EnemyBullet bulletGO = Instantiate (bulletPrefab, spawnspot.position, Quaternion.identity) as EnemyBullet;
        bulletGO.Init(20f, 1f);
    }
}
