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
    private float height = 0.2f;
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
        ResetGameValues();
        ResetScores();
        //Debug.Log("<color=yellow>best height</color>" + PlayerPrefs.GetFloat("BestHeight", 0.2f));
    }

    private void Update ()
    {
        RestartForPC();
    }

    #region Reset
    private void ResetScores()
    {
        if (resetScoresControl)
        {
            PlayerPrefs.DeleteKey("BestScore");
            PlayerPrefs.DeleteKey("BestHeight");
        }
    }

    private void ResetGameValues()
    {
        GameSpeed("assign", defaultSpeedValue);

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
    public void GameSpeed(string state, float newSpeedValue)
    {
        if (state == "default")
        {
            gameSpeedValue = newSpeedValue;
            gameSpeedText.text = "Speed: " + RoundValue(gameSpeedValue).ToString();
        }
        else if (state == "assign")
        {
            Time.timeScale = newSpeedValue;
            gameSpeedText.text = "Speed: " + RoundValue(Time.timeScale).ToString();
        }
        else if (state == "operation")
        {
            gameSpeedValue += newSpeedValue;
            Time.timeScale = gameSpeedValue;
            gameSpeedText.text = "Speed: " + RoundValue(gameSpeedValue).ToString();
        }
        //Debug.Log(Time.timeScale);
    }
    #endregion

    #region Height Info
    public void AssignHeight()
    {
        if (panel.transform.localScale.y <= 5f)
        {
            panel.transform.localScale += new Vector3(0f, 0.2f, 0f); //increase panel's height scale
            gameObject.transform.GetComponent<DistanceControl>().SetDistance(); //keep object's distance according to panel
        }

        height += 0.2f;
        heightText.text = RoundValue(height).ToString();

        ShowBestHeight();
    }

    private void ShowBestHeight()
    {
        if (height > PlayerPrefs.GetFloat("BestHeight", 0.2f))
        {
            newBestCountForHeight += 1;
            //Debug.Log("<color=green>new best count for height:</color>" + newBestCountForHeight);
            StartCoroutine(ShowNewBest(1));
            PlayerPrefs.SetFloat("BestHeight", RoundValue(height));
            bestHeightText.text = "Best \nHeight " + "\n" + RoundValue(height).ToString(); //en iyi yükseklik

#if UNITY_ANDROID
            string heightStr = string.Format("{0:0.0000}", height);
            long longHeight = long.Parse(heightStr.Replace(".", ""));
            GooglePlayControl.AddScoreToLeaderboard(GPGSIds.leaderboard_best_heights, longHeight);
#endif
        }
    }
    #endregion

    #region Score Info
    public void AssignScore(string state, float scoreValue)
    {
        if (state == "lottery")
        {
            score += scoreValue;
        }
        else if (state == "regular")
        {
            score += scoreValue * (comboCounter * 0.25f);
        }

        scoreText.text = RoundValue(score).ToString();
        ShowBestScore();
    }

    private void ShowBestScore()
    {
        if (score > PlayerPrefs.GetFloat("BestScore", 0f))
        {
            while (newBestCountForScore <= 3)
            {
                newBestCountForScore += 1;
                //Debug.Log("<color=blue>new best count for score:</color>" + newBestCountForScore);
                StartCoroutine(ShowNewBest(2));
            }
        }
    }

    private void AssignBestScore()
    {
        if (score > PlayerPrefs.GetFloat("BestScore", 0f))
        {
            PlayerPrefs.SetFloat("BestScore", RoundValue(score));
            bestScoreText.text = "Best \nScore " + "\n" + RoundValue(score).ToString(); //en iyi skor

#if UNITY_ANDROID
            string scoreString = string.Format("{0:0.0000}", score);
            long longScore = long.Parse(scoreString.Replace(".", ""));
            GooglePlayControl.AddScoreToLeaderboard(GPGSIds.leaderboard_best_scores, longScore);
#endif
        }
    }
    #endregion

    #region New Best
    private IEnumerator ShowNewBest(int height1Orscore2)
    {

        if (height1Orscore2 == 1 & 
            PlayerPrefs.GetFloat("BestHeight", 0.2f) != 0.2f &
            newBestCountForHeight == 1)
        {
            newBestHeight.enabled = true;
            FindObjectOfType<SFXControl>().SesOynat("NewBestSound");
            yield return new WaitForSeconds(5f);
            newBestHeight.enabled = false;
        }
        else if (height1Orscore2 == 2 & 
            PlayerPrefs.GetFloat("BestScore", 0f) != 0f & 
            newBestCountForScore == 1)
        {
            newBestScore.enabled = true;
            FindObjectOfType<SFXControl>().SesOynat("NewBestSound");
            yield return new WaitForSeconds(5f);
            newBestScore.enabled = false;
        }
    }
    #endregion

    #region Restart Game
    public void RestartForMobile()
    {
        if (restartControl)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            restartControl = false;
        }
    }

    private void RestartForPC()
    {
        if (restartControl & Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            restartControl = false;
        }
    }
    #endregion

    #region Game Over
    public void GameOver(bool gameOverControl)
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
            AssignBestScore();
            GameSpeed("assign", 0f);
            gameOverControl = false;
            restartControl = true;

            ShowAd();
        }
    }
    #endregion

    #region Show Ad and LeaderBoard
    private void ShowAd()
    {
        gameOverCounter = PlayerPrefs.GetInt("oyunBittiSayac");
        gameOverCounter++;
        PlayerPrefs.SetInt("oyunBittiSayac", gameOverCounter);
        //Debug.Log(gameOverCounter);

        if (gameOverCounter == 5)
        {
#if UNITY_ANDROID
            adControl.ShowAd();
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
    private void ComboSystem()
    {
        if (comboCounter == 0)
        {
            comboTextRight.enabled = false;
            comboTextLeft.enabled = false;
        }
        else if (comboCounter % 2 == 0)
        {
            comboTextRight.text = "PerFit x " + comboCounter.ToString();
            comboTextRight.enabled = true;
            comboTextLeft.enabled = false;
        }
        else if (comboCounter % 2 == 1)
        {
            comboTextLeft.text = "PerFit x " + comboCounter.ToString();
            comboTextRight.enabled = false;
            comboTextLeft.enabled = true;
        }
    }

    public void ComboCount(string state)
    {
        if (state == "reset")
        {
            comboCounter = 0;
        }
        else if (state == "inc")
        {
            comboCounter++;
        }

        ComboSystem();
    }
    #endregion

    private float RoundValue(float _value)
    {
        float value = _value < 0 ? 0 : (Mathf.Round(_value * 100f) / 100f);
        return value;

        //if (_value < 0) //eğer reduce sonrası value 0'ın altına düşerse value olarak 0 gönder.
        //{
        //    return 0;
        //}
        //else
        //{
        //    return (Mathf.Round(_value * 100f) / 100f);
        //}
    }
}
