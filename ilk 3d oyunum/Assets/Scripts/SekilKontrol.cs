using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SekilKontrol : MonoBehaviour {

    [Range(0f, 1f)]
    public float donmeHizi;

    float donmeYonu;
    bool sekliPaneleSabitleKontrol = false;

    GameObject Panel;
    GameObject cutPoint;
    GameObject fitPoint;
    OyunKontrol oyunKontrol;

    void Start()
    {
        objeBul();
        sekilDonmeYonu();
    }

    void Update()
    {
        sekilDondur();
        paneleSabitle();
    }

    private void objeBul()
    {
        Panel    = GameObject.FindGameObjectWithTag("panelTag");
        cutPoint = GameObject.FindGameObjectWithTag("cutPointTag");
        fitPoint = GameObject.FindGameObjectWithTag("fitPointTag");
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<OyunKontrol>();
    }

    void sekilDonmeYonu()
    {
        donmeYonu = Random.value <= 0.5f ? 1 : -1;

        //if (Random.value <= 0.5f) //random.value 0 ile 1 arasında random deger secer
        //    donmeYonu = 1; //sola donmesi icin
        //else
        //    donmeYonu = -1; // saga donmesi icin
    }

    void sekilDondur()
    {
        if (!sekliPaneleSabitleKontrol)
        {
            transform.Rotate(new Vector3(0, Random.Range(0, 360f), 0) * ((Time.deltaTime * donmeYonu) * donmeHizi));
        }
    }

    void paneleSabitle() //objeleri panele sabitleyerek onunla birlikte hereket etmesini sağlayan fonksiyon
    {
        if (sekliPaneleSabitleKontrol)
        {
            transform.localRotation = Panel.transform.localRotation; //şeklin panele sabitlenmesi
        }

        cutPoint.transform.localRotation = Panel.transform.localRotation; //cutpointin panele sabitlenmesi
        fitPoint.transform.localRotation = Panel.transform.localRotation; //fitpointin panele sabitlenmesi
    }

    void OnTriggerEnter(Collider other) //gecirgen yuzeye temas ettiginde
    {
        if (other.name == "FitPoint")
        {
            Panel.GetComponent<PanelKontrol>().panelParticleOynat(true);
            FindObjectOfType<SesKontrol>().sesOynat("FitSound");
            donmeHizi = 0f;
            Panel.transform.localScale += new Vector3(0f, 0.2f, 0f);
            oyunKontrol.score += 1;
            sekliPaneleSabitleKontrol = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "cutPointTag")
        {
            Destroy(gameObject);
        }
    }

}
