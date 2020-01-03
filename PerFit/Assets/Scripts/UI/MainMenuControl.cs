using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {

    [Header("Buttons")]
    public Button playButton;
    public Button backButton, homeButton, nextButton;

    [Header("Texts")]
    public Text versionCode;

    [Header("Canvas")]
    public Canvas mainMenuCanvas;
    public Canvas tutorialCanvas;

    [Header("Gameobjects")]
    public GameObject buttonsForMobile;
    public GameObject buttonsForPc;
    public GameObject tutorialPage1;
    public GameObject tutorialPage2;

    private void Awake()
    {
//#if UNITY_EDITOR
        //buttonsForPc.SetActive(true);
#if UNITY_WEBGL
        buttonsForPc.SetActive(true);
        buttonsForMobile.SetActive(false);
#elif UNITY_ANDROID
        buttonsForMobile.SetActive(true);
        buttonsForPc.SetActive(false);
#else
        Debug.Log("platform bulunamadı");
#endif
    }

    public void OyunuBaslat()
    {
        TransitionControl.transitionManager.FadeToNextScene();
    }

    public void OpenCanvas(string whichCanvas)
    {
        if (whichCanvas == "tutorial")
        {
            mainMenuCanvas.enabled = false;
            tutorialCanvas.enabled = true;
        }
        else if (whichCanvas == "leaderboard")
        {
            mainMenuCanvas.enabled = false;
        }
    }

    public void ActionsForButtons(string whichButton)
    {
        if (whichButton == "back")
        {
            tutorialPage2.SetActive(false);
            tutorialPage1.SetActive(true);
            backButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
        else if (whichButton == "home")
        {
            mainMenuCanvas.enabled = true;
            tutorialCanvas.enabled = false;
        }
        else if (whichButton == "next")
        {
            tutorialPage1.SetActive(false);
            tutorialPage2.SetActive(true);
            backButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
    }

#if UNITY_ANDROID
    public void ShowLeaderboards()
    {
        GooglePlayControl.ShowLeaderboardsUI();
    }
#endif
}
