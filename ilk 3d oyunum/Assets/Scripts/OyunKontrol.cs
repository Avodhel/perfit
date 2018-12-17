using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour {

    [Range(1f, 10f)]
    public float oyunHizi = 1f;
    public GameObject oyunBittiPanel;

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

    public void oyunBitti(bool oyunBittiKontrol)
    {
        if (oyunBittiKontrol)
        {
            oyunBittiPanel.SetActive(true);
            oyunHizi = 0f;
            oyunBittiKontrol = false;
        }
    }
}
