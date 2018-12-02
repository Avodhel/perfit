using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKontrol : MonoBehaviour {

    public GameObject sekil;
    GameObject mainCamera;
    Vector3 aradakiMesafe;
    GameObject cutPoint;
    Vector3 aradakiMesafe2;

	void Start ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cutPoint = GameObject.FindGameObjectWithTag("cutPointTag");

        aradakiMesafe = transform.position - mainCamera.transform.position;
        aradakiMesafe2 = cutPoint.transform.position - transform.position;
        
        StartCoroutine(sekilOlustur());
    }

    void LateUpdate()
    {
        transform.position = mainCamera.transform.position + aradakiMesafe; //spawnpointin kamera ile birlikte hareket etmesi
        cutPoint.transform.position = transform.position + aradakiMesafe2; //cut pointin spawn point ile birlikte hareket etmesi
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
