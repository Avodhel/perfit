using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelKontrol : MonoBehaviour {

    [Range(1f, 250f)]
    public float panelHareketHizi;
    //public Text heightText;
    public TMP_Text heightText;
    //public Text bestHeightText;
    public TMP_Text bestHeightText;

    Vector3 panelBoyut;
    float yukseklik;
    string yukseklikStr;

    OyunKontrol oyunKontrol;

    private float cycleSeconds = 500f;

    void Start()
    {
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<OyunKontrol>();
        bestHeightText.text = "Best " + PlayerPrefs.GetFloat("BestHeight", 0f).ToString() + " M";
        //PlayerPrefs.DeleteAll();
    }

    void Update ()
    {
        yukseklikGoster();
        panelHareket();
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
            heightText.text = yukseklikStr.Substring(0, i) + " M"; //substring ile i kadar basamağı göster
        }

        eniyiyukseklikGoster();
    }

    void eniyiyukseklikGoster()
    {
        if (yukseklik > PlayerPrefs.GetFloat("BestHeight", 0f))
        {
            PlayerPrefs.SetFloat("BestHeight", yukseklik);
            bestHeightText.text = "Best " + yukseklik.ToString() + " M"; //en iyi yükseklik
        }
    }

    void panelHareket()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * panelHareketHizi * Time.deltaTime, 0); //sağa veya sola döndür
    }

    public void panelRenkDegistir(float renkSinirR, float renkSinirG, float renkSinirB)
    {
        GetComponent<Renderer>().material.color = Color.HSVToRGB
                (
                Mathf.Repeat((Time.time + renkSinirR) / cycleSeconds, 1f),
                renkSinirG,     // set to a pleasing value. 0f to 1f
                renkSinirB      // set to a pleasing value. 0f to 1f
                );
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
            Debug.Log("Up");
            oyunKontrol.oyunBitti(true);
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
