using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKontrol : MonoBehaviour {

    GameObject fitPoint;
    Vector3 aradakiMesafe;

	void Start ()
    {
        fitPoint = GameObject.FindGameObjectWithTag("fitPointTag");
        aradakiMesafe = transform.position - fitPoint.transform.position;
	}
	
	void LateUpdate ()
    {
        transform.position = fitPoint.transform.position + aradakiMesafe; //kameranın fitpointle birlikte hareket etmesi
	}
}
