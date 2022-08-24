using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    [SerializeField] Transform spawnspot;
    [SerializeField] CharacterBullet[] bulletPrefab;
    [SerializeField] BulletShower showerPrefab;
    public BulletScatter scatter;
    float Timer = 2;
    
    [SerializeField] private GameManager gameManager;
    
    private void Awake() 
    {
        //PlayerPrefs.SetInt("SpaceshipEquiped", PlayerPrefs.GetInt("CurrentSpaceship"));
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0f)
        {
            //kind of bullets
            if (gameManager.playerSO.player.equipedSpaceship == 0) //normal bullet
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[0]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(100f, 1f);
                Timer = 0.3f;
            }
            else if(gameManager.playerSO.player.equipedSpaceship == 1) //knock back enemy
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[1]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(70f, 1f);
                Timer = 0.3f;
            }
            else if (gameManager.playerSO.player.equipedSpaceship == 2) //slow enemy
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[2]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(50f, 1f);
                Timer = 0.3f;
            }
            else if (gameManager.playerSO.player.equipedSpaceship == 3) //penetrate enemy
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[3]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(70f, 1f);
                Timer = 0.3f;
            }
            
            if (gameManager.playerSO.player.equipedSpaceship == 4) //normal bullet
            {
                CharacterBullet bulletGO = Instantiate(bulletPrefab[4]);
                bulletGO.transform.position = spawnspot.position;
                bulletGO.Init(70f, 1f);
                Timer = 0.3f;
            }
            
            else if (gameManager.playerSO.player.equipedSpaceship == 5) //shower
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