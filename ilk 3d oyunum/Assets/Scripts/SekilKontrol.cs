using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SekilKontrol : MonoBehaviour {

    float donmeHizi = 0.1f;
    float donmeYonu = 1f;
    GameObject Panel;
    bool sekliPaneleSabitleKontrol = false;
    GameObject cutPoint;

    void Start()
    {
        Panel = GameObject.FindGameObjectWithTag("panelTag");
        cutPoint = GameObject.FindGameObjectWithTag("cutPointTag");
        sekilDonmeYonu();
    }

    void Update ()
    {
        sekilDondur();
        paneleSabitle();
	}

    void sekilDonmeYonu()
    {
        //Debug.Log(Random.value);
        if (Random.value <= 0.5f) //random.value 0 ile 1 arasında random deger secer
            donmeYonu = 1; //sola donmesi icin
        else
            donmeYonu = -1; // saga donmesi icin
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
    }

    void OnTriggerEnter(Collider other) //gecirgen yuzeye temas ettiginde
    {
        if (other.name == "FitPoint")
        {
            donmeHizi = 0f;
            Panel.transform.localScale += new Vector3(0f, 0.2f, 0f);
            sekliPaneleSabitleKontrol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "cutPointTag")
        {
            Destroy(gameObject);
        }
    }

}
