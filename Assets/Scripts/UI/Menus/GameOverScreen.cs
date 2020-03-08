using UnityEngine;
using System;
using TMPro;

public class GameOverScreen : CanvasController {

    private MoveUI moveUI;
    private TextMeshProUGUI playtimeUI;
    private TextMeshProUGUI highscoreUI;
    private GameController gameController;
    private PauseBackground pauseBackground;

    // Statistics

    // Balls
    private TextMeshProUGUI ballsCollisionsUI;
    private TextMeshProUGUI ballsTotalDamageUI;
    private TextMeshProUGUI ballsFiredUI;

    // Benitrat0r
    private TextMeshProUGUI totalBetsUI;
    private TextMeshProUGUI averageBetUI;

    // Enemies
    private TextMeshProUGUI enemiesKilledUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        gameController = FindObjectOfType<GameController>();
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");

        // Balls
        ballsCollisionsUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BallStatistics/Collisions/Value");
        ballsTotalDamageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BallStatistics/TotalDamage/Value");
        ballsFiredUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BallStatistics/Fired/Value");

        // Benitrat0r
        totalBetsUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BenitratorStatistics/TotalBets/Value");
        averageBetUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BenitratorStatistics/AverageBet/Value");

        // Enemies
        enemiesKilledUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/EnemyStatistics/Kills/Value");

        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    private void UpdateStatisticsUI() {

        // Balls
        ballsCollisionsUI.text = Statistics.Instance.balls.collisions.ToString();
        ballsTotalDamageUI.text = Statistics.Instance.balls.damageDealt.ToString();
        ballsFiredUI.text = Statistics.Instance.balls.fired.ToString();

        // Benitrat0r
        totalBetsUI.text = Statistics.Instance.benitrator.totalBets.ToString();
        averageBetUI.text =  Statistics.Instance.benitrator.AverageBet.ToString("0.##");

        // Enemies
        enemiesKilledUI.text = Statistics.Instance.enemies.killed.ToString();
    }

    public override void Show() {
        SaveHighscore();
        UpdateStatisticsUI();
        pauseBackground.disableInteractability = true;
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        moveUI.FadeTo(new Vector2(0f, 55f), 0.5f, true);
    }

    private void SaveHighscore() {
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;
        highscoreUI.text = Score.instance.highscore.ToString();
        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp);
    }
}
