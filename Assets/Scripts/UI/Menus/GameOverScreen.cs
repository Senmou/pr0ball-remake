using System;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class GameOverScreen : CanvasController {

    private MoveUI moveUI;
    private TextMeshProUGUI playtimeUI;
    private TextMeshProUGUI highscoreUI;
    private StatisticsMenu statisticsMenu;
    private GameController gameController;
    private PauseBackground pauseBackground;
    private HighscoreController highscoreController;
    private SimpleStatisticsMenu simpleStatisticsMenu;


    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        statisticsMenu = FindObjectOfType<StatisticsMenu>();
        gameController = FindObjectOfType<GameController>();
        pauseBackground = FindObjectOfType<PauseBackground>();
        highscoreController = FindObjectOfType<HighscoreController>();
        simpleStatisticsMenu = FindObjectOfType<SimpleStatisticsMenu>();
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
    }

    public override void Show() {
        pauseBackground.disableInteractability = true;
        LeanTween.moveY(gameObject, 0f, showEaseDuration)
            .setIgnoreTimeScale(true);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        LeanTween.moveY(gameObject, 55f, hideEaseDuration)
            .setIgnoreTimeScale(true)
            .setEase(hideEaseType)
            .setOnComplete(() => gameObject.SetActive(false));
    }

    public void ShowStatistics() {
        statisticsMenu.SetStatistics(Statistics.Instance);
        simpleStatisticsMenu.SetStatistics(Statistics.Instance);
        CanvasManager.instance.SwitchCanvas(CanvasType.SIMPLE_STATISTICS_MENU);
    }

    public void SaveHighscore() {
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;
        highscoreUI.text = Score.instance.highscore.ToString();
        Statistics stats = Statistics.Instance;
        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp, stats.GetCopy<Statistics>());

        //Analytics.CustomEvent("gameOver", new Dictionary<string, object> {
        //    { "level", LevelData.Level }
        //});

        AnalyticsResult ar = Analytics.CustomEvent("MyEvent");
        Debug.Log("Result = " + ar.ToString());

        highscoreController.UploadHighscore(Score.instance.highscore);
    }
}
