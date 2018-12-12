using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunKontrol : MonoBehaviour {

    [Range(1f, 10f)]
    public float oyunHizi = 1f;

	void Start ()
    {
        Time.timeScale = 1f;
	}
	
	void Update ()
    {
        oyunHiziAyarla();
    }

    private void oyunHiziAyarla()
    {
        Time.timeScale = oyunHizi;
    }
}
