#if UNITY_WEBGL
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLeaderBoard : MonoBehaviour {

    public Text[] highscoreFields;
    LeaderBoard leaderBoardManager;

    void Start()
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = i + 1 + ". Fetching...";
        }


        leaderBoardManager = GetComponent<LeaderBoard>();
        StartCoroutine("RefreshHighscores");
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = i + 1 + ". ";
            if (i < highscoreList.Length)
            {
                highscoreFields[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            leaderBoardManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}
#endif