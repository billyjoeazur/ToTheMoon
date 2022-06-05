using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxCamRandom : MonoBehaviour
{
    public GameObject[] skyboxex;

    
    void Start()
    {
        int rand = Random.Range(0, skyboxex.Length);
        skyboxex[rand].gameObject.SetActive(true);
    }
}
