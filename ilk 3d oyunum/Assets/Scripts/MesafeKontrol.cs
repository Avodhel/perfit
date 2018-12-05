using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesafeKontrol : MonoBehaviour {

    GameObject topPoint;
    GameObject fitPoint;
    GameObject cutPoint;
    GameObject spawnPoint;
    GameObject kamera;
    GameObject dLight;

    Vector3 fitVeTopAradakiMesafe;
    Vector3 cutVeTopAradakiMesafe;
    Vector3 spawnVeTopAradakiMesafe;
    Vector3 kameraVeTopAradakiMesafe;
    Vector3 lightVeTopAradakiMesafe;

    void Start ()
    {
        objeBul();
        mesafeHesapla();
	}

	void LateUpdate ()
    {
        mesafeUygula();
	}

    void objeBul()
    {
        topPoint   = GameObject.FindGameObjectWithTag("topPointTag");
        fitPoint   = GameObject.FindGameObjectWithTag("fitPointTag");
        cutPoint   = GameObject.FindGameObjectWithTag("cutPointTag");
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPointTag");
        kamera     = GameObject.FindGameObjectWithTag("MainCamera");
        dLight     = GameObject.FindGameObjectWithTag("lightTag");
    }

    void mesafeHesapla()
    {
        fitVeTopAradakiMesafe    = fitPoint.transform.position   - topPoint.transform.position;
        cutVeTopAradakiMesafe    = cutPoint.transform.position   - topPoint.transform.position;
        spawnVeTopAradakiMesafe  = spawnPoint.transform.position - topPoint.transform.position;
        kameraVeTopAradakiMesafe = kamera.transform.position     - topPoint.transform.position;
        lightVeTopAradakiMesafe  = dLight.transform.position     - topPoint.transform.position;
    }

    void mesafeUygula()
    {
        fitPoint.transform.position   = topPoint.transform.position + fitVeTopAradakiMesafe;
        cutPoint.transform.position   = topPoint.transform.position + cutVeTopAradakiMesafe;
        spawnPoint.transform.position = topPoint.transform.position + spawnVeTopAradakiMesafe;
        kamera.transform.position     = topPoint.transform.position + kameraVeTopAradakiMesafe;
        dLight.transform.position     = topPoint.transform.position + lightVeTopAradakiMesafe;
    }
}
