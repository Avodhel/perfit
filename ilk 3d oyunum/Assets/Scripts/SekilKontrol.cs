using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SekilKontrol : MonoBehaviour {

    float donmeHizi = 0.1f;
    float donmeYonu = 1f;
    GameObject Panel;
    bool sekliPaneleSabitleKontrol = false;

    void Start()
    {
        Panel = GameObject.FindGameObjectWithTag("panelTag");
        sekilDonmeYonu();
    }

    void Update ()
    {
        sekilDondur();
        sekliPaneleSabitle();
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

    void sekliPaneleSabitle()
    {
        if (sekliPaneleSabitleKontrol)
        {
            transform.localRotation = Panel.transform.localRotation;
        }
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

    //void OnTriggerExit(Collider other) //gecirgen yuzey ile olan temas bittiginde
    //{
    //    if (other.name == "FitPoint")
    //    {
    //        Debug.Log(transform.position.y);
    //        Panel.transform.localScale = new Vector3(1.7f, transform.position.y + 0.2f, 1.7f);
    //    }
    //}

    //void OnCollisionEnter(Collision collision) //gecirgen olmayan yuzeye temas edildiginde
    //{
    //    if (
    //        collision.transform.tag == "squareTag")
    //    {
    //        Debug.Log(transform.position.y);
    //    }
    //}

}
