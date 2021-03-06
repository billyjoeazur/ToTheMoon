using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    [SerializeField] Transform spawnspot;
    [SerializeField] CharacterBullet[] bulletPrefab;
    [SerializeField] BulletShower showerPrefab;

    float Timer = 2;

    void Start()
    {
        //PlayerPrefs.SetString("CurrentGun", "gun4");
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        PlayerPrefs.SetInt("SpaceshipEquiped", PlayerPrefs.GetInt("CurrentSpaceship"));

        if (Timer <= 0f)
        {
            //kind of bullets
            if (PlayerPrefs.GetInt("SpaceshipEquiped") == 0) //normal bullet
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[0]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(100f, 1f);
                Timer = 0.3f;
            }
            else if(PlayerPrefs.GetInt("SpaceshipEquiped") == 1) //knock back enemy
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[1]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(70f, 1f);
                Timer = 0.3f;
            }
            else if (PlayerPrefs.GetInt("SpaceshipEquiped") == 2) //slow enemy
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[2]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(50f, 1f);
                Timer = 0.3f;
            }
            else if (PlayerPrefs.GetInt("SpaceshipEquiped") == 3) //penetrate enemy
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[3]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(70f, 1f);
                Timer = 0.3f;
            }
            
            if (PlayerPrefs.GetInt("SpaceshipEquiped") == 4) //normal bullet
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[4]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(70f, 1f);
                Timer = 0.3f;
            }
            
            else if (PlayerPrefs.GetInt("SpaceshipEquiped") == 5) //shower
            {
                // CharacterBullet bulletGO = Instantiate(bulletPrefab[0]);
                // bulletGO.transform.position = spawnspot.position;
                // bulletGO.Init(20f, 1f);
                
                BulletShower shower = Instantiate(showerPrefab, transform.position, Quaternion.identity);
                Timer = 0.1f;
            }


        }

    }
}
