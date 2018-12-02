using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKontrol : MonoBehaviour {

    GameObject fitPoint;
    Vector3 kameraVeFitAradakiMesafe;

	void Start ()
    {
        fitPoint = GameObject.FindGameObjectWithTag("fitPointTag");

        kameraVeFitAradakiMesafe = transform.position - fitPoint.transform.position;
	}
	
	void LateUpdate ()
    {
        transform.position = fitPoint.transform.position + kameraVeFitAradakiMesafe; //kameranın fitpointle birlikte hareket etmesi
	}

}
