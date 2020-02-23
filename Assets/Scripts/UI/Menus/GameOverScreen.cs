using System;
using TMPro;
using UnityEngine;

public class GameOverScreen : CanvasController {

    private MoveUI moveUI;
    private TextMeshProUGUI playtimeUI;
    private TextMeshProUGUI highscoreUI;
    private GameController gameController;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        gameController = FindObjectOfType<GameController>();
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");
    }

    public override void Show() {
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
        UpdateUI();
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(0f, 40f), 0.5f);
    }

    private void UpdateUI() {
        highscoreUI.text = Score.instance.highscore.ToString();
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;

        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp);
    }
}
