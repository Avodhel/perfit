using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour {

    [Header("Game Speed")]
    [Range(1f, 10f)]
    public float oyunHizi;

    [Header("Reset Scores")]
    public bool resetScoresControl;

    [Header("Panels")]
    public GameObject oyunBittiPanel;

    [Header("Buttons")]
    public GameObject buttonsForMobile;
    public GameObject buttonsForPc;

    [Header("Texts")]
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public TMP_Text heightText;
    public TMP_Text bestHeightText;
    public TMP_Text newBestHeight;
    public TMP_Text newBestScore;

    [HideInInspector]
    public float score = 0f;

    GameObject panel;
    Vector3 panelBoyut;

#if UNITY_ANDROID
    ReklamKontrol reklamKontrol;
#endif

    bool restartKontrol = false;
    bool assignNewBestScoreControl = false;
    float yukseklik;
    string yukseklikStr;
    int newBestCountForHeight = 0;
    int newBestCountForScore = 0;
    int oyunBittiSayac;

    void Awake()
    {
        panel = GameObject.FindGameObjectWithTag("panelTag");
#if UNITY_ANDROID
        reklamKontrol = GameObject.FindGameObjectWithTag("reklamKontrolTag").GetComponent<ReklamKontrol>();
#endif
        //Debug.Log("<color=gray>awake best height</color>" + PlayerPrefs.GetFloat("BestHeight", 0.2f));
        PlayerPrefs.SetFloat("oyunHizi", oyunHizi);
    }

    void Start ()
    {
        Time.timeScale = 1f;
        scoreText.text = "" + score;
        bestHeightText.text = "Best \nHeight " + "\n" + PlayerPrefs.GetFloat("BestHeight", 0f).ToString();
        bestScoreText.text = "Best \nScore " + "\n" + PlayerPrefs.GetFloat("BestScore", 0f);
        newBestCountForHeight = 0;
        //Debug.Log("<color=green>new best count for height:</color>" + newBestCountForHeight);
        newBestCountForScore = 0;
        //Debug.Log("<color=blue>new best count for score:</color>" + newBestCountForHeight);
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
            PlayerPrefs.DeleteKey("BestScore");
            PlayerPrefs.DeleteKey("BestHeight");
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
            //Debug.Log("<color=green>new best count for height:</color>" + newBestCountForHeight);
            StartCoroutine(showNewBest(1));
            PlayerPrefs.SetFloat("BestHeight", yukseklik);
            bestHeightText.text = "Best \nHeight " + "\n" + yukseklik.ToString(); //en iyi yükseklik

#if UNITY_ANDROID
            string yukseklikString = string.Format("{0:0.0000}", yukseklik);
            long longHeight = long.Parse(yukseklikString.Replace(".", ""));
            GooglePlayKontrol.AddScoreToLeaderboard(GPGSIds.leaderboard_best_heights, longHeight);
#endif
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
            while (newBestCountForScore <= 3)
            {
                newBestCountForScore += 1;
                //Debug.Log("<color=blue>new best count for score:</color>" + newBestCountForScore);
                StartCoroutine(showNewBest(2));
            }
        }

        if (score > PlayerPrefs.GetFloat("BestScore", 0f) & assignNewBestScoreControl)
        {
            PlayerPrefs.SetFloat("BestScore", score);
            bestScoreText.text = "Best \nScore " + "\n" + score; //en iyi skor

#if UNITY_ANDROID
            string scoreString = string.Format("{0:0.0000}", score);
            long longScore = long.Parse(scoreString.Replace(".", ""));
            GooglePlayKontrol.AddScoreToLeaderboard(GPGSIds.leaderboard_best_scores, longScore);
#endif
        }
    }

    IEnumerator showNewBest(int height1Orscore2)
    {

        if (height1Orscore2 == 1 & 
            PlayerPrefs.GetFloat("BestHeight", 0.2f) != 0.2f &
            newBestCountForHeight == 1)
        {
            newBestHeight.enabled = true;
            FindObjectOfType<SesKontrol>().sesOynat("NewBestSound");
            yield return new WaitForSeconds(5f);
            newBestHeight.enabled = false;
        }
        else if (height1Orscore2 == 2 & 
            PlayerPrefs.GetFloat("BestScore", 0f) != 0f & 
            newBestCountForScore == 1)
        {
            newBestScore.enabled = true;
            FindObjectOfType<SesKontrol>().sesOynat("NewBestSound");
            yield return new WaitForSeconds(5f);
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

#if UNITY_WEBGL
            buttonsForPc.SetActive(true);
            buttonsForMobile.SetActive(false);
#elif UNITY_ANDROID
            buttonsForMobile.SetActive(true);
            buttonsForPc.SetActive(false);
#else
        Debug.Log("platform bulunamadı");
#endif

            assignNewBestScoreControl = true;
            oyunHizi = 0f;
            oyunBittiKontrol = false;
            restartKontrol = true;

            reklamGoster();
        }
    }

    public void yenidenBaslatMobile()
    {
        if (restartKontrol)
        {
            SceneManager.LoadScene("Scene_1");
            restartKontrol = false;
        }
    }

    void yenidenBaslatPc()
    {
        if (restartKontrol & Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Scene_1");
            restartKontrol = false;
        }
    }

    void reklamGoster()
    {
        oyunBittiSayac = PlayerPrefs.GetInt("oyunBittiSayac");
        oyunBittiSayac++;
        PlayerPrefs.SetInt("oyunBittiSayac", oyunBittiSayac);
        Debug.Log(oyunBittiSayac);

        if (oyunBittiSayac == 5)
        {
#if UNITY_ANDROID
            reklamKontrol.reklamiGoster();
#endif
            PlayerPrefs.SetInt("oyunBittiSayac", 0);
        }
    }

#if UNITY_ANDROID
    public void ShowLeaderboards()
    {
        GooglePlayKontrol.ShowLeaderboardsUI();
    }
#endif
}
