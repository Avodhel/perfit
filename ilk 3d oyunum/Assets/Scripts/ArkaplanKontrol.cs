using UnityEngine;

public class ArkaplanKontrol : MonoBehaviour {

    private float cycleSeconds = 500f; // set to say 0.5f to test

    float renkSinirR;
    float renkSinirG;
    float renkSinirB;

    PanelKontrol panelKontrol;
    SpawnKontrol spawnKontrol;

    void Start()
    {
        panelKontrol = GameObject.FindGameObjectWithTag("panelTag").GetComponent<PanelKontrol>();
        spawnKontrol = GameObject.FindGameObjectWithTag("spawnPointTag").GetComponent<SpawnKontrol>();
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

    public void arkaplanRenkDegistir()
    {
        GetComponent<Renderer>().material.color = Color.HSVToRGB
            (
             Mathf.Repeat((Time.time + renkSinirR) / cycleSeconds, 1f),
             renkSinirG,     // set to a pleasing value. 0f to 1f
             renkSinirB      // set to a pleasing value. 0f to 1f
            );

        objelerRenkDegistir();
    }

    private void objelerRenkDegistir()
    {
        panelKontrol.panelRenkDegistir(renkSinirR, renkSinirG, renkSinirB); //arkaplan ile birlikte panelin de rengini degistir
        spawnKontrol.sekilRenkDegistir(renkSinirR, renkSinirG, renkSinirB); //arkaplan ile square'in de rengini degistir
    }
}
