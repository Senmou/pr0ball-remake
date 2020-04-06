using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;

public class GlobalHighscores : MonoBehaviour {

    private void Start() {
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        //PlayGamesPlatform.InitializeInstance(config);
        //PlayGamesPlatform.Activate();

        //Social.localUser.Authenticate(success => {

        //});
    }

    public static void AddScoreToLeaderboard(long score) {
        Social.ReportScore(score, GPGSIds.leaderboard_global_highscores, success => {

        });
    }

    public static void ShowLeaderboardUI() {
        Social.ShowLeaderboardUI();
    }

    public void OnClickGlobalHighscoreButton() {
        ShowLeaderboardUI();
    }
}
