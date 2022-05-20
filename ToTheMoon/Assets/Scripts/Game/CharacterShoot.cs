using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    [SerializeField]
    Transform spawnspot;

    [SerializeField]
    CharacterBullet[] bulletPrefab;

    [SerializeField]
    BulletRicochet ricochetPrefab;


    float Timer = 2;

    void Start()
    {
        
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        PlayerPrefs.SetString("MyGun", PlayerPrefs.GetString("CurrentGun"));


        if (Timer <= 0f)
        {
            //kind of bullets
            if (PlayerPrefs.GetString("MyGun") == "")
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[0]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(20f, 1f);
                Timer = 0.3f;
            }
            else if(PlayerPrefs.GetString("MyGun") == "gun2")
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[1]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(20f, 1f);
                Timer = 0.3f;
            }
            else if (PlayerPrefs.GetString("MyGun") == "gun3")
            {
                //Bullet bulletGO = Instantiate(bulletPrefab[2]);
                //bulletGO.transform.position = spawnspot.position;
                //bulletGO.Init(20f, 1f);
                
                BulletRicochet scatter = Instantiate(ricochetPrefab, transform.position, Quaternion.identity);
                Timer = 0.1f;
            }
            else if (PlayerPrefs.GetString("MyGun") == "gun4")
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[3]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(20f, 1f);
                Timer = 0.3f;
            }
            else if (PlayerPrefs.GetString("MyGun") == "gun5")
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[4]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(20f, 1f);
                Timer = 0.3f;
            }


        }

    }
}
