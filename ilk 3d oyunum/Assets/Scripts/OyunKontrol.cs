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
    public TMP_Text newBestHeight;
    public TMP_Text newBestScore;
    public bool mobilKontrol;
    public bool resetScoresControl;
    [HideInInspector]
    public float score = 0f;

    GameObject panel;
    Vector3 panelBoyut;

    bool restartKontrol = false;
    float yukseklik;
    string yukseklikStr;
    int newBestCountForHeight = 0;
    int newBestCountForScore = 0;

    //bool assignNewBestScoreControl = false;

    void Awake()
    {
        panel = GameObject.FindGameObjectWithTag("panelTag");
        panel.GetComponent<PanelKontrol>().mobilKontrol = mobilKontrol;
        //Debug.Log("<color=gray>awake best height</color>" + PlayerPrefs.GetFloat("BestHeight", 0.2f));
    }

    void Start ()
    {
        Time.timeScale = 1f;
        scoreText.text = "" + score;
        bestHeightText.text = "Best \nHeight " + "\n" + PlayerPrefs.GetFloat("BestHeight", 0f).ToString();
        bestScoreText.text = "Best \nScore " + "\n" + PlayerPrefs.GetFloat("BestScore", 0f);
        newBestCountForHeight = 0;
        Debug.Log("<color=green>new best count for height:</color>" + newBestCountForHeight);
        newBestCountForScore = 0;
        Debug.Log("<color=blue>new best count for score:</color>" + newBestCountForHeight);

        resetScores();

        //Debug.Log("<color=yellow>best height</color>" + PlayerPrefs.GetFloat("BestHeight", 0.2f));
    }

    void Update ()
    {
        oyunHiziAyarla();
        yenidenBaslatPc();
        scoreGoster();
        yukseklikGoster();

    }

    private void resetScores()
    {
        if (resetScoresControl)
        {
            PlayerPrefs.DeleteAll();
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
        if (yukseklik > PlayerPrefs.GetFloat("BestHeight", 0.2f))
        {
            newBestCountForHeight += 1;
            Debug.Log("<color=green>new best count for height:</color>" + newBestCountForHeight);
            StartCoroutine(showNewBest(1));
            PlayerPrefs.SetFloat("BestHeight", yukseklik);
            bestHeightText.text = "Best \nHeight " + "\n" + yukseklik.ToString(); //en iyi yükseklik
        }
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
            newBestCountForScore += 1;
            Debug.Log("<color=blue>new best count for score:</color>" + newBestCountForScore);
            StartCoroutine(showNewBest(2));

            //if (assignNewBestScoreControl)
            //{
            PlayerPrefs.SetFloat("BestScore", score);
            //}

            bestScoreText.text = "Best \nScore " + "\n" + score; //en iyi skor
        }
    }

    IEnumerator showNewBest(int height1Orscore2)
    {

        if (height1Orscore2 == 1 & 
            PlayerPrefs.GetFloat("BestHeight", 0.2f) != 0.2f &
            newBestCountForHeight == 1)
        {
            newBestHeight.enabled = true;
            yield return new WaitForSeconds(2.5f);
            newBestHeight.enabled = false;
        }
        else if (height1Orscore2 == 2 & 
            PlayerPrefs.GetFloat("BestScore", 0f) != 0f & 
            newBestCountForScore == 1)
        {
            newBestScore.enabled = true;
            yield return new WaitForSeconds(2.5f);
            newBestScore.enabled = false;
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

            //assignNewBestScoreControl = true;
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
