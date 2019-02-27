using UnityEngine;

public class AnaMenuArkaplanKontrol : MonoBehaviour
{

    private float cycleSeconds = 500f; // set to say 0.5f to test

    float renkSinirR;
    float renkSinirG;
    float renkSinirB;

    void Start()
    {
        renkSinirBelirle();
    }

    void Update()
    {
        arkaplanRenkDegistir();
    }

    public void renkSinirBelirle()
    {
        renkSinirR = Random.Range(1f, 359f);
        renkSinirG = Random.Range(0.3f, 0.8f);
        renkSinirB = Random.Range(0.3f, 0.8f);
    }

    void arkaplanRenkDegistir()
    {
        if (gameObject.name == "Arkaplan")
        {
            GetComponent<Renderer>().material.color = Color.HSVToRGB
                (
                Mathf.Repeat((Time.time + renkSinirR) / cycleSeconds, 1f),
                renkSinirG,     // set to a pleasing value. 0f to 1f
                renkSinirB      // set to a pleasing value. 0f to 1f
                );
        }
    }
}
