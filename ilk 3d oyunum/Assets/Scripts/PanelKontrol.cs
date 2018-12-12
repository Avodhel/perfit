using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelKontrol : MonoBehaviour {

    [Tooltip("1 ile 250 arası degistirilebilir")]
    [Range(1f, 250f)]
    public float panelHareketHizi;
    public Text heightText;

    Vector3 panelBoyut;
    float yukseklik;
    string yukseklikStr;

    public Transform brokenObject;

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
    }

    void panelHareket()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * panelHareketHizi * Time.deltaTime, 0); //sağa veya sola döndür
    }

    void OnCollisionEnter(Collision col) //panele carpan sekillerin hangi acidan carptigini saptama
    {
        paneleCarpmaAcisi(col);
    }

    void paneleCarpmaAcisi(Collision col)//panele carpan sekillerin hangi acidan carptigini saptama
    {
        if (col.gameObject.tag.Equals("squareTag"))
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
                brokeGlass(col);
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

    public void brokeGlass(Collision col)
    {
        Destroy(col.gameObject);
        Instantiate(brokenObject, col.transform.position, col.transform.rotation);
        brokenObject.localScale = col.transform.localScale;
    }
}
