using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkaplanKontrol : MonoBehaviour {

    private float cycleSeconds = 500f; // set to say 0.5f to test
    float renkSinir1;
    float renkSinir2;

    private void Start()
    {
        renkSinirBelirle();
    }

    void Update()
    {
        arkaplanRenkDegistir();
    }

    public void renkSinirBelirle()
    {
        renkSinir1 = Random.Range(0.4f, 0.8f);
        renkSinir2 = Random.Range(0.4f, 0.8f);
    }

    void arkaplanRenkDegistir()
    {
        GetComponent<Renderer>().material.color = Color.HSVToRGB
            (
             Mathf.Repeat(Time.time / cycleSeconds, 1f),
             renkSinir1,     // set to a pleasing value. 0f to 1f
             renkSinir2      // set to a pleasing value. 0f to 1f
            );
    }
}
