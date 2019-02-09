using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelKontrol : MonoBehaviour {

    [HideInInspector]
    public bool mobilKontrol;

    [Range(1f, 250f)]
    public float panelHareketHizi;
    //public Text heightText;
    public TMP_Text heightText;
    //public Text bestHeightText;
    public TMP_Text bestHeightText;
    public ParticleSystem panelParticle;

    private float cycleSeconds = 500f;

    float yukseklik;
    string yukseklikStr;
    float rotSpeed = 12f;

    Vector3 panelBoyut;
    OyunKontrol oyunKontrol;
    Material bottomPointMat;
    Material cutPointMat;

    void Start()
    {
        objeBul();
        bestHeightText.text = "Best " + PlayerPrefs.GetFloat("BestHeight", 0f).ToString();
        //PlayerPrefs.DeleteAll();
    }

    void Update ()
    {
        yukseklikGoster();
        panelHareket();
    }

    void objeBul()
    {
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<OyunKontrol>();
        bottomPointMat = GameObject.FindGameObjectWithTag("bottomPointTag").GetComponent<Renderer>().material;
        cutPointMat = GameObject.FindGameObjectWithTag("cutPointTag").GetComponent<Renderer>().material;
    }

    void yukseklikGoster()
    {
        panelBoyut = transform.localScale;
        yukseklik = panelBoyut.y;
        yukseklikStr = yukseklik.ToString(); //yukseklik bilgisini string'e çevir
        for (int i = 0; i <= 5; i++)
        {
            if (yukseklikStr.Length < i) //stringin uzunluğu i'den küçükse hata verme, devam et
            {
                continue;
            }
            heightText.text = yukseklikStr.Substring(0, i); //substring ile i kadar basamağı göster
        }

        eniyiyukseklikGoster();
    }

    void eniyiyukseklikGoster()
    {
        if (yukseklik > PlayerPrefs.GetFloat("BestHeight", 0f))
        {
            PlayerPrefs.SetFloat("BestHeight", yukseklik);
            bestHeightText.text = "Best " + yukseklik.ToString(); //en iyi yükseklik
        }
    }

    void panelHareket()
    {
        if (mobilKontrol)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) //mobilde sağa ve sola döndür
            {
                Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.Rotate(0, -(touchDeltaPosition.x) * rotSpeed * Time.deltaTime, 0);
            }
        }
        else
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * panelHareketHizi * Time.deltaTime, 0); //sağa veya sola döndür(mobilde çalışmıyor)
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
        //Debug.Log(hit);
        float angle = Vector3.Angle(hit, Vector3.up);

        if (Mathf.Approximately(angle, 0))
        {
            //Down
            //Debug.Log("Down");
        }
        if (Mathf.Approximately(angle, 180))
        {
            //Up
            //Debug.Log("Up");
            FindObjectOfType<SesKontrol>().sesOynat("HitSound");
            //oyunKontrol.oyunBitti(true);
        }
        if (Mathf.Approximately(angle, 90))
        {
            // Sides
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
            if (cross.y > 0)
            { // left side of the player
              //Debug.Log("Left");
            }
            else
            { // right side of the player
              //Debug.Log("Right");
            }
        }
    }
}
