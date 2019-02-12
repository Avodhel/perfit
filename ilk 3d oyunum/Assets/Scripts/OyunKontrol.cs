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
    public bool mobilKontrol;
    public bool resetScores;

    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    GameObject panel;

    bool restartKontrol = false;

    [HideInInspector]
    public float score =  0f;

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
