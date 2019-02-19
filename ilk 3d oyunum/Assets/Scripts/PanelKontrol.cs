using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelKontrol : MonoBehaviour {

    [HideInInspector]
    public bool mobilKontrol;
    public bool reverseActive = false;
    [Range(1f, 250f)]
    public float panelHareketHizi;
    public ParticleSystem panelParticle;

    private float cycleSeconds = 500f;

    float rotSpeed = 12f;

    Material bottomPointMat;
    Material cutPointMat;
    ChanceKontrol chanceKontrol;

    void Start()
    {
        objeBul();
    }

    void Update ()
    {
        panelHareket();
    }

    void objeBul()
    {
        bottomPointMat = GameObject.FindGameObjectWithTag("bottomPointTag").GetComponent<Renderer>().material;
        cutPointMat = GameObject.FindGameObjectWithTag("cutPointTag").GetComponent<Renderer>().material;

        chanceKontrol = GameObject.FindGameObjectWithTag("chanceKontrolTag").GetComponent<ChanceKontrol>();
    }

    void panelHareket()
    {
        int donusYonuAta;
        if (!reverseActive)
        {
            donusYonuAta = 1; //normal
        }
        else
        {
            donusYonuAta = -1; //ters
        }

        if (mobilKontrol)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) //mobilde sağa ve sola döndür
            {
                Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.Rotate(0, donusYonuAta * (touchDeltaPosition.x) * rotSpeed * Time.deltaTime, 0);
            }
        }
        else
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * panelHareketHizi * Time.deltaTime * donusYonuAta, 0); //sağa veya sola döndür(mobilde çalışmıyor)
        }
    }

    public void panelRenkDegistir(float renkSinirR, float renkSinirG, float renkSinirB)
    {
        GetComponent<Renderer>().material.color = Color.HSVToRGB
                (
                Mathf.Repeat((Time.time + renkSinirR) / cycleSeconds, 1f),
                renkSinirG,     // set to a pleasing value. 0f to 1f
                renkSinirB      // set to a pleasing value. 0f to 1f
                );

        bottomPointMat.color = Color.HSVToRGB
                (
                Mathf.Repeat((Time.time + renkSinirR) / cycleSeconds, 1f),
                renkSinirG,     // set to a pleasing value. 0f to 1f
                renkSinirB      // set to a pleasing value. 0f to 1f
                );

        cutPointMat.color = Color.HSVToRGB
                (
                Mathf.Repeat((Time.time + renkSinirR) / cycleSeconds, 1f),
                renkSinirG,     // set to a pleasing value. 0f to 1f
                renkSinirB      // set to a pleasing value. 0f to 1f
                );
    }

    public void panelParticleOynat(bool particleKontrol)
    {
        if (particleKontrol)
        {
            panelParticle.Play();
        }
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag.Equals("squareTag"))
        {
            carpmaAcisiSaptama(col);
        }
    }

    void carpmaAcisiSaptama(Collision col) //panele carpan sekillerin hangi acidan carptigini saptama
    {
        Vector3 hit = col.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);

        if (Mathf.Approximately(angle, 180)) //panele üstten çarptıysa
        {
            FindObjectOfType<SesKontrol>().sesOynat("HitSound");
            StartCoroutine(chanceKontrol.chanceControlFunc()); //chance kalıp kalmadığını kontrol et
        }
    }

}
