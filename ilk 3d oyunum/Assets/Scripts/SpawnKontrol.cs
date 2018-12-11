using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKontrol : MonoBehaviour {

    public GameObject sekil, glass;
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
            Instantiate(glass, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90f));

            yield return new WaitForSeconds(spawnSuresi);
        }
    }
}
