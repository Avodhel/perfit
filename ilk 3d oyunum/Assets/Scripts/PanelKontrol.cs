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

    OyunKontrol oyunKontrol;
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
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<OyunKontrol>();
        bottomPointMat = GameObject.FindGameObjectWithTag("bottomPointTag").GetComponent<Renderer>().material;
        cutPointMat = GameObject.FindGameObjectWithTag("cutPointTag").GetComponent<Renderer>().material;

        chanceKontrol = GameObject.FindGameObjectWithTag("chanceKontrolTag").GetComponent<ChanceKontrol>();
    }

    void panelHareket()
    {
        if (!reverseActive)
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
                transform.Rotate(0, Input.GetAxis("Horizontal") * panelHareketHizi * Time.deltaTime * -1, 0); //sağa veya sola döndür(mobilde çalışmıyor)
            }
        }
        else
        {
            if (mobilKontrol)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) //mobilde sağa ve sola döndür
                {
                    Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    transform.Rotate(0, (touchDeltaPosition.x) * rotSpeed * Time.deltaTime, 0);
                }
            }
            else
            {
                transform.Rotate(0, Input.GetAxis("Horizontal") * panelHareketHizi * Time.deltaTime, 0); //sağa veya sola döndür(mobilde çalışmıyor)
            }
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
            StartCoroutine(chanceControlFunc(chanceKontrol.chanceCounter));
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

    IEnumerator chanceControlFunc(int chanceCounter)
    {
        if (chanceCounter == 0)
        {
            oyunKontrol.oyunBitti(true);
        }
        else
        {
            oyunKontrol.oyunHizi = 0.5f;
            chanceKontrol.chanceIncOrRed("red");
            chanceKontrol.brokenChanceFunc(true);
            yield return new WaitForSeconds(2f);
            chanceKontrol.brokenChanceFunc(false);
            oyunKontrol.oyunHizi = 1.75f;
        }
    }
}
