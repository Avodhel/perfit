using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour {

    [Header("Game Speed")]
    [Range(1f, 10f)]
    public float gameSpeedValue = 1.75f;

    [Header("Game Objects")]
    public GameObject panel;

    [Header("Reset Scores")]
    public bool resetScoresControl;

    [Header("Panels")]
    public GameObject gameOverPanel;

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
    public Text gameSpeedText;
    public Text comboTextLeft;
    public Text comboTextRight;

    [HideInInspector]
    public float score = 0f;
    [HideInInspector]
    public float defaultSpeedValue;

    private Vector3 panelScale;

    private int comboCounter = 0;

#if UNITY_ANDROID
    AdControl adControl;
#endif

    private bool restartControl = false;
    private float height;
    private int newBestCountForHeight = 0;
    private int newBestCountForScore = 0;
    private int gameOverCounter;

    public static GameControl gameManager { get; private set; }

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

#if UNITY_ANDROID
        adControl = GameObject.FindGameObjectWithTag("reklamKontrolTag").GetComponent<AdControl>();
#endif
    }

    private void Start ()
    {
        defaultSpeedValue = gameSpeedValue; //put default game speed
        resetGameValues();
        resetScores();
        //Debug.Log("<color=yellow>best height</color>" + PlayerPrefs.GetFloat("BestHeight", 0.2f));
    }

    private void Update ()
    {
        restartForPC();
    }

    #region Reset
    private void resetScores()
    {
        if (resetScoresControl)
        {
            PlayerPrefs.DeleteKey("BestScore");
            PlayerPrefs.DeleteKey("BestHeight");
        }
    }

    private void resetGameValues()
    {
        gameSpeed("assign", defaultSpeedValue);

        scoreText.text = score.ToString();
        bestHeightText.text = "Best \nHeight " + "\n" + PlayerPrefs.GetFloat("BestHeight", 0f).ToString();
        bestScoreText.text = "Best \nScore " + "\n" + PlayerPrefs.GetFloat("BestScore", 0f).ToString();

        newBestCountForHeight = 0;
        //Debug.Log("<color=green>new best count for height:</color>" + newBestCountForHeight);
        newBestCountForScore = 0;
        //Debug.Log("<color=blue>new best count for score:</color>" + newBestCountForHeight);
    }
    #endregion

    #region Game Speed
    public void gameSpeed(string state, float newSpeedValue)
    {
        if (state == "default")
        {
            gameSpeedValue = newSpeedValue;
            gameSpeedText.text = roundValue(gameSpeedValue).ToString();
        }
        else if (state == "assign")
        {
            Time.timeScale = newSpeedValue;
            gameSpeedText.text = roundValue(Time.timeScale).ToString();
        }
        else if (state == "operation")
        {
            gameSpeedValue += newSpeedValue;
            Time.timeScale = gameSpeedValue;
            gameSpeedText.text = roundValue(gameSpeedValue).ToString();
        }
        //Debug.Log(Time.timeScale);
    }
    #endregion

    #region Height Info
    public void assignHeight()
    {
        panel.transform.localScale += new Vector3(0f, 0.2f, 0f);
        panelScale = panel.transform.localScale;
        height = panelScale.y;
        heightText.text = roundValue(height).ToString();

        showBestHeight();
    }

    private void showBestHeight()
    {
        if (height > PlayerPrefs.GetFloat("BestHeight", 0.2f))
        {
            newBestCountForHeight += 1;
            //Debug.Log("<color=green>new best count for height:</color>" + newBestCountForHeight);
            StartCoroutine(showNewBest(1));
            PlayerPrefs.SetFloat("BestHeight", height);
            bestHeightText.text = "Best \nHeight " + "\n" + roundValue(height).ToString(); //en iyi yükseklik

#if UNITY_ANDROID
            string heightStr = string.Format("{0:0.0000}", height);
            long longHeight = long.Parse(heightStr.Replace(".", ""));
            GooglePlayControl.AddScoreToLeaderboard(GPGSIds.leaderboard_best_heights, longHeight);
#endif
        }
    }
    #endregion

    #region Score Info
    public void assignScore(string state, float scoreValue)
    {
        if (state == "lottery")
        {
            score += scoreValue;
        }
        else if (state == "regular")
        {
            score += scoreValue * (comboCounter * 0.25f);
        }

        scoreText.text = roundValue(score).ToString();
        showBestScore();
    }

    private void showBestScore()
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
    }

    private void assignBestScore()
    {
        if (score > PlayerPrefs.GetFloat("BestScore", 0f))
        {
            PlayerPrefs.SetFloat("BestScore", score);
            bestScoreText.text = "Best \nScore " + "\n" + roundValue(score).ToString(); //en iyi skor

#if UNITY_ANDROID
            string scoreString = string.Format("{0:0.0000}", score);
            long longScore = long.Parse(scoreString.Replace(".", ""));
            GooglePlayControl.AddScoreToLeaderboard(GPGSIds.leaderboard_best_scores, longScore);
#endif
        }
    }
    #endregion

    #region New Best
    private IEnumerator showNewBest(int height1Orscore2)
    {

        if (height1Orscore2 == 1 & 
            PlayerPrefs.GetFloat("BestHeight", 0.2f) != 0.2f &
            newBestCountForHeight == 1)
        {
            newBestHeight.enabled = true;
            FindObjectOfType<SFXControl>().sesOynat("NewBestSound");
            yield return new WaitForSeconds(5f);
            newBestHeight.enabled = false;
        }
        else if (height1Orscore2 == 2 & 
            PlayerPrefs.GetFloat("BestScore", 0f) != 0f & 
            newBestCountForScore == 1)
        {
            newBestScore.enabled = true;
            FindObjectOfType<SFXControl>().sesOynat("NewBestSound");
            yield return new WaitForSeconds(5f);
            newBestScore.enabled = false;
        }
    }
    #endregion

    #region Restart Game
    public void restartForMobile()
    {
        if (restartControl)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            restartControl = false;
        }
    }

    private void restartForPC()
    {
        if (restartControl & Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            restartControl = false;
        }
    }
    #endregion

    #region Game Over
    public void gameOver(bool gameOverControl)
    {
        if (gameOverControl)
        {
            gameOverPanel.SetActive(true);

#if UNITY_WEBGL
            buttonsForPc.SetActive(true);
            buttonsForMobile.SetActive(false);
#elif UNITY_ANDROID
            buttonsForMobile.SetActive(true);
            buttonsForPc.SetActive(false);
#else
        Debug.Log("platform bulunamadı");
#endif
            assignBestScore();
            gameSpeed("assign", 0f);
            gameOverControl = false;
            restartControl = true;

            showAd();
        }
    }
    #endregion

    #region Show Ad and LeaderBoard
    private void showAd()
    {
        gameOverCounter = PlayerPrefs.GetInt("oyunBittiSayac");
        gameOverCounter++;
        PlayerPrefs.SetInt("oyunBittiSayac", gameOverCounter);
        //Debug.Log(gameOverCounter);

        if (gameOverCounter == 5)
        {
#if UNITY_ANDROID
            adControl.showAd();
#endif
            PlayerPrefs.SetInt("oyunBittiSayac", 0);
        }
    }

#if UNITY_ANDROID
    public void ShowLeaderboards()
    {
        GooglePlayControl.ShowLeaderboardsUI();
    }
#endif
    #endregion

    #region Combo System
    private void comboSystem()
    {
        if (comboCounter == 0)
        {
            comboTextRight.enabled = false;
            comboTextLeft.enabled = false;
        }
        else if (comboCounter % 2 == 0)
        {
            comboTextRight.text = "Combo x " + comboCounter.ToString();
            comboTextRight.enabled = true;
            comboTextLeft.enabled = false;
        }
        else if (comboCounter % 2 == 1)
        {
            comboTextLeft.text = "Combo x " + comboCounter.ToString();
            comboTextRight.enabled = false;
            comboTextLeft.enabled = true;
        }
    }

    public void comboCount(string state)
    {
        if (state == "reset")
        {
            comboCounter = 0;
        }
        else if (state == "inc")
        {
            comboCounter++;
        }

        comboSystem();
    }
    #endregion

    private float roundValue(float value)
    {
        if (value < 0) //eğer reduce sonrası value 0'ın altına düşerse value olarak 0 gönder.
        {
            return 0;
        }
        else
        {
            return (Mathf.Round(value * 100f) / 100f);
        }
    }
}
