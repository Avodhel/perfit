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
    public Text pressRtoRestartText;
    public bool mobilKontrol;

    GameObject panel;

    bool restartKontrol = false;

    void Awake()
    {
        panel = GameObject.FindGameObjectWithTag("panelTag");
        panel.GetComponent<PanelKontrol>().mobilKontrol = mobilKontrol;
    }

    void Start ()
    {
        Time.timeScale = 1f;
	}
	
	void Update ()
    {
        oyunHiziAyarla();
        yenidenBaslatPc();
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
            if (mobilKontrol)
            {
                playRestartButton.gameObject.SetActive(true);
            }
            else
            {
                pressRtoRestartText.gameObject.SetActive(true);
            }
            
            oyunHizi = 0f;
            oyunBittiKontrol = false;
            restartKontrol = true;
        }
    }

    public void yenidenBaslat()
    {
        if (restartKontrol & mobilKontrol)
        {
            SceneManager.LoadScene("Scene_1");
            restartKontrol = false;
        }
    }

    void yenidenBaslatPc()
    {
        if (restartKontrol & !mobilKontrol & Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Scene_1");
            restartKontrol = false;
        }
    }
}
