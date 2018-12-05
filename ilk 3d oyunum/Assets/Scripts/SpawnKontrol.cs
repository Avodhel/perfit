using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKontrol : MonoBehaviour {

    public GameObject sekil;

	void Start ()
    {        
        StartCoroutine(sekilOlustur());
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
