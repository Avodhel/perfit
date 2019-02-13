using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OyunKontrol : MonoBehaviour {

    [Range(1f, 10f)]
    public float oyunHizi = 1f;
    public GameObject oyunBittiPanel;
    public Button playRestartButton;
    public Text pressRtoRestartText;
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public TMP_Text heightText;
    public TMP_Text bestHeightText;
    public bool mobilKontrol;
    public bool resetScores;
    [HideInInspector]
    public float score = 0f;

    GameObject panel;
    Vector3 panelBoyut;

    bool restartKontrol = false;
    float yukseklik;
    string yukseklikStr;

    void Awake()
    {
        panel = GameObject.FindGameObjectWithTag("panelTag");
        panel.GetComponent<PanelKontrol>().mobilKontrol = mobilKontrol;
    }

    void Start ()
    {
        Time.timeScale = 1f;
        scoreText.text = "" + score;
        bestScoreText.text = "Best \nScore " + "\n" + PlayerPrefs.GetFloat("BestScore", 0f);
        bestHeightText.text = "Best \nHeight " + "\n" + PlayerPrefs.GetFloat("BestHeight", 0f).ToString();

        if (resetScores)
        {
            PlayerPrefs.DeleteAll();
        }
    }
	
	void Update ()
    {
        oyunHiziAyarla();
        yenidenBaslatPc();
        scoreGoster();
        yukseklikGoster();
    }

    void scoreGoster()
    {
        scoreText.text = "" + score;

        eniyiscoregoster();
    }

    void eniyiscoregoster()
    {
        if (score > PlayerPrefs.GetFloat("BestScore", 0f))
        {
            PlayerPrefs.SetFloat("BestScore", score);
            bestScoreText.text = "Best \nScore " + "\n" + score; //en iyi skor
        }
    }

    void yukseklikGoster()
    {
        panelBoyut = panel.transform.localScale;
        yukseklik = panelBoyut.y;
        yukseklikStr = yukseklik.ToString(); //yukseklik bilgisini string'e çevir
        for (int i = 0; i <= 5; i++)
        {
            if (yukseklikStr.Length < i) //stringin uzunluğu i'den küçükse hata verme, devam et
            {
                continue;
            }
            heightText.text = yukseklikStr.Substring(0, i); //substring ile i kadar basamağı göster
        }

        eniyiyukseklikGoster();
    }

    void eniyiyukseklikGoster()
    {
        if (yukseklik > PlayerPrefs.GetFloat("BestHeight", 0f))
        {
            PlayerPrefs.SetFloat("BestHeight", yukseklik);
            bestHeightText.text = "Best \nHeight " + "\n" + yukseklik.ToString(); //en iyi yükseklik
        }
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
