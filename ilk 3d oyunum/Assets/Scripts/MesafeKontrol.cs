using UnityEngine;

public class MesafeKontrol : MonoBehaviour {

    GameObject topPoint;
    GameObject fitPoint;
    GameObject cutPoint;
    GameObject spawnPoint;
    GameObject kamera;
    GameObject dLight;
    GameObject arkaplan;
    GameObject particleSys;
    GameObject panelParticle;

    Vector3 fitVeTopAradakiMesafe;
    Vector3 cutVeTopAradakiMesafe;
    Vector3 spawnVeTopAradakiMesafe;
    Vector3 kameraVeTopAradakiMesafe;
    Vector3 lightVeTopAradakiMesafe;
    Vector3 arkaplanVeTopAradakiMesafe;
    Vector3 particleSysVeTopAradakiMesafe;
    Vector3 panelParticleVeTopAradakiMesafe;

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
        topPoint       = GameObject.FindGameObjectWithTag("topPointTag");
        fitPoint       = GameObject.FindGameObjectWithTag("fitPointTag");
        cutPoint       = GameObject.FindGameObjectWithTag("cutPointTag");
        spawnPoint     = GameObject.FindGameObjectWithTag("spawnPointTag");
        kamera         = GameObject.FindGameObjectWithTag("MainCamera");
        dLight         = GameObject.FindGameObjectWithTag("lightTag");
        arkaplan       = GameObject.FindGameObjectWithTag("arkaplanTag");
        particleSys    = GameObject.FindGameObjectWithTag("particleSystemTag");
        panelParticle  = GameObject.FindGameObjectWithTag("panelParticleTag");
    }

    void mesafeHesapla()
    {
        fitVeTopAradakiMesafe            = fitPoint.transform.position      - topPoint.transform.position;
        cutVeTopAradakiMesafe            = cutPoint.transform.position      - topPoint.transform.position;
        spawnVeTopAradakiMesafe          = spawnPoint.transform.position    - topPoint.transform.position;
        kameraVeTopAradakiMesafe         = kamera.transform.position        - topPoint.transform.position;
        lightVeTopAradakiMesafe          = dLight.transform.position        - topPoint.transform.position;
        arkaplanVeTopAradakiMesafe       = arkaplan.transform.position      - topPoint.transform.position;
        particleSysVeTopAradakiMesafe    = particleSys.transform.position   - topPoint.transform.position;
        panelParticleVeTopAradakiMesafe  = panelParticle.transform.position - topPoint.transform.position;
    }

    void mesafeUygula()
    {
        fitPoint.transform.position       = topPoint.transform.position + fitVeTopAradakiMesafe;
        cutPoint.transform.position       = topPoint.transform.position + cutVeTopAradakiMesafe;
        spawnPoint.transform.position     = topPoint.transform.position + spawnVeTopAradakiMesafe;
        kamera.transform.position         = topPoint.transform.position + kameraVeTopAradakiMesafe;
        dLight.transform.position         = topPoint.transform.position + lightVeTopAradakiMesafe;
        arkaplan.transform.position       = topPoint.transform.position + arkaplanVeTopAradakiMesafe;
        particleSys.transform.position    = topPoint.transform.position + particleSysVeTopAradakiMesafe;
        panelParticle.transform.position  = topPoint.transform.position + panelParticleVeTopAradakiMesafe;
    }
}
