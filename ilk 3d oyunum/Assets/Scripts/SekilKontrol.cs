using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SekilKontrol : MonoBehaviour {

	void Start ()
    {
		
	}

	void Update ()
    {
        sekilDondur();
	}

    void sekilDondur()
    {
        transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0) * (Time.deltaTime * 0.1f));
    }
}
