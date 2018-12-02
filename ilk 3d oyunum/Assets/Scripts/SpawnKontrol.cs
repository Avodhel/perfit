using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKontrol : MonoBehaviour {

    public GameObject sekil;
    GameObject mainCamera;
    Vector3 spawnVeKameraAradakiMesafe;
    GameObject cutPoint;
    Vector3 cutVeSpawnAradakiMesafe;

	void Start ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cutPoint = GameObject.FindGameObjectWithTag("cutPointTag");

        spawnVeKameraAradakiMesafe = transform.position - mainCamera.transform.position;
        cutVeSpawnAradakiMesafe = cutPoint.transform.position - transform.position;
        
        StartCoroutine(sekilOlustur());
    }

    void LateUpdate()
    {
        transform.position = mainCamera.transform.position + spawnVeKameraAradakiMesafe; //spawnpointin kamera ile birlikte hareket etmesi
        cutPoint.transform.position = transform.position + cutVeSpawnAradakiMesafe; //cutpointin spawn point ile birlikte hareket etmesi
    }

    IEnumerator sekilOlustur()
    {
        while (true)
        {
            Instantiate(sekil, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(10);
        }
    }
}
