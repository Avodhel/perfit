using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GooglePlayKontrol : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    void SignIn()
    {
        Social.localUser.Authenticate(success => {
            if (success)
            {
                ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
                Debug.Log("Login Success");
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }

    //#region Achievements
    //public static void UnlockAchievement(string id)
    //{
    //    Social.ReportProgress(id, 100, success => { });
    //}

    //public static void IncrementAchievement(string id, int stepsToIncrement)
    //{
    //    PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    //}

    //public static void ShowAchievementsUI()
    //{
    //    Social.ShowAchievementsUI();
    //}
    //#endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        if (Social.localUser.authenticated) //google service'e giriş yapıldıysa
        {
            Social.ReportScore(score, leaderboardId, success =>
            {
            if (success)
            {
                Debug.Log("score eklendi");
            }
            else
            {
                Debug.Log("score eklenmedi");
            }
            });
        }
        else
        {
            Debug.Log("for this option you should sign in google play service");
        }
    }

    public static void ShowLeaderboardsUI()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
            //PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIh47lmIAcEAIQAA");
            Debug.Log("leaderboard gösterildi");
        }
        else
        {
            Debug.Log("kullanıcı girişi yapılmadı");
        }
    }
    #endregion /Leaderboards

}
