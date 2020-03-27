using UnityEngine;
using System;
using TMPro;

public class GameOverScreen : CanvasController {

    private MoveUI moveUI;
    private TextMeshProUGUI playtimeUI;
    private TextMeshProUGUI highscoreUI;
    private StatisticsMenu statisticsMenu;
    private GameController gameController;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        statisticsMenu = FindObjectOfType<StatisticsMenu>();
        gameController = FindObjectOfType<GameController>();
        pauseBackground = FindObjectOfType<PauseBackground>();
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
    }

    public override void Show() {
        pauseBackground.disableInteractability = true;
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        moveUI.FadeTo(new Vector2(0f, 55f), 0.5f, true);
    }

    public void ShowStatistics() {
        statisticsMenu.SetStatistics(Statistics.Instance);
        CanvasManager.instance.SwitchCanvas(CanvasType.STATISTICS);
    }

    public void SaveHighscore() {
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;
        highscoreUI.text = Score.instance.highscore.ToString();
        Statistics stats = Statistics.Instance;
        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp, stats.GetCopy<Statistics>());
    }
}
