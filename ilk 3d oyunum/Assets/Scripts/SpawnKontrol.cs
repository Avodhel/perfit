using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKontrol : MonoBehaviour {

    public GameObject sekil;

    public GameObject[] ozelSekiller;
    GameObject ozelSekil;

    [Range(1, 50)]
    public int spawnSuresi;

    [Range(1, 20)]
    public int kacSekildeBirOzelSekil;

    private float cycleSeconds = 500f;

    GameObject square;
    GameObject[] squares;

    bool squareOlustuKontrol = false;
    bool renkDegistiKontrol = false;

    int sekilCount = 0;

    void Start ()
    {        
        StartCoroutine(sekilOlustur());
    }

    IEnumerator sekilOlustur()
    {
        while (true)
        {
            sekilCount += 1;

            if (sekilCount == kacSekildeBirOzelSekil)
            {
                ozelSekil = ozelSekiller[Random.Range(0, ozelSekiller.Length)];
                Instantiate(ozelSekil,
                            transform.position,
                            Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z));

                sekilCount = 0;
            }
            else if(sekilCount < kacSekildeBirOzelSekil)
            {
                Instantiate(sekil,
                            transform.position,
                            Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z));
            }

            squares = GameObject.FindGameObjectsWithTag("squareTag");

            squareOlustuKontrol = true;
            renkDegistiKontrol = false;

            yield return new WaitForSeconds(spawnSuresi);

        }
    }


    public void sekilRenkDegistir(float renkSinirR, float renkSinirG, float renkSinirB)
    {

        if (squareOlustuKontrol && !renkDegistiKontrol)
        {
            for (int i = 0; i < squares.Length; i++)
            {
                squares[i].GetComponent<Renderer>().material.color = Color.HSVToRGB
                        (
                        Mathf.Repeat((Time.time + renkSinirR) / cycleSeconds, 1f),
                        renkSinirG,     // set to a pleasing value. 0f to 1f
                        renkSinirB      // set to a pleasing value. 0f to 1f
                        );
            }
            renkDegistiKontrol = true;
        }
    }
}
