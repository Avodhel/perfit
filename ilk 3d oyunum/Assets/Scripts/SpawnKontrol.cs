using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKontrol : MonoBehaviour {

    public GameObject sekil;
    GameObject mainCamera;
    Vector3 aradakiMesafe;

	void Start ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        aradakiMesafe = transform.position - mainCamera.transform.position;
        
        StartCoroutine(sekilOlustur());
    }

    void LateUpdate()
    {
        transform.position = mainCamera.transform.position + aradakiMesafe; //spawnpointin kamera ile birlikte hareket etmesi
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
