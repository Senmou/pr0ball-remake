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
    private TextMeshProUGUI totalBetsUI;
    private TextMeshProUGUI averageBetUI;
    private TextMeshProUGUI collisionsUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        gameController = FindObjectOfType<GameController>();
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");

        totalBetsUI = transform.FindChild<TextMeshProUGUI>("Statistics/Values/TotalBets/Value");
        averageBetUI = transform.FindChild<TextMeshProUGUI>("Statistics/Values/AverageBet/Value");
        collisionsUI = transform.FindChild<TextMeshProUGUI>("Statistics/Values/Collisions/Value");

        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    public override void Show() {
        SaveHighscore();

        collisionsUI.text = Statistics.Instance.balls.collisions.ToString();
        totalBetsUI.text = Statistics.Instance.benitrator.totalBets.ToString();
        averageBetUI.text = Statistics.Instance.benitrator.AverageBet.ToString();

        pauseBackground.disableInteractability = true;
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        moveUI.FadeTo(new Vector2(0f, 40f), 0.5f, true);
    }

    private void SaveHighscore() {
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;
        highscoreUI.text = Score.instance.highscore.ToString();
        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp);
    }
}
