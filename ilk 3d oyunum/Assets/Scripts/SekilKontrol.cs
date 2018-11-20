using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SekilKontrol : MonoBehaviour {

    float donmeHizi = 0.1f;
    float donmeYonu = 1f;
    GameObject Panel;

    void Start()
    {
        Panel = GameObject.FindGameObjectWithTag("panelTag");
        sekilDonmeYonu();
    }

    void Update ()
    {
        sekilDondur();
	}

    void sekilDonmeYonu()
    {
        Debug.Log(Random.value);
        if (Random.value <= 0.5f) //random.value 0 ile 1 arasında random deger secer
            donmeYonu = 1; //sola donmesi icin
        else
            donmeYonu = -1; // saga donmesi icin
    }

    void sekilDondur()
    {
        transform.Rotate(new Vector3(0, Random.Range(0, 360f), 0) * ((Time.deltaTime * donmeYonu) * donmeHizi));
    }

    void OnTriggerEnter(Collider other) //gecirgen yuzeye temas ettiginde
    {
        if (other.name == "FitPoint")
        {
            donmeHizi = 0f;
            Panel.transform.localScale += new Vector3(0f, 0.2f, 0f);
        }
    }
}
