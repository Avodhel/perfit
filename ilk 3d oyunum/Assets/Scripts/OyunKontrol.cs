using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OyunKontrol : MonoBehaviour {

    [Range(1f, 10f)]
    public float oyunHizi = 1f;
    public GameObject oyunBittiPanel;
    public Button playRestartButton;

    bool restartKontrol = false;

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
            playRestartButton.gameObject.SetActive(true); 
            oyunHizi = 0f;
            oyunBittiKontrol = false;
            restartKontrol = true;
        }
    }

    public void yenidenBaslat()
    {
        if (restartKontrol)
        {
            SceneManager.LoadScene("Scene_1");
            restartKontrol = false;
        }
    }
}
