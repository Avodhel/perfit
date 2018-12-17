using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKontrol : MonoBehaviour {

    public GameObject sekil;
    [Range(1, 50)]
    public int spawnSuresi;

	void Start ()
    {        
        StartCoroutine(sekilOlustur());
    }

    IEnumerator sekilOlustur()
    {
        while (true)
        {
            Instantiate(sekil, 
                        transform.position, 
                        Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z));

            yield return new WaitForSeconds(spawnSuresi);
        }
    }
}
