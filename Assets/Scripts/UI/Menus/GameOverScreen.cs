using UnityEngine;
using System;
using TMPro;

public class GameOverScreen : CanvasController {

    private MoveUI moveUI;
    private TextMeshProUGUI playtimeUI;
    private TextMeshProUGUI highscoreUI;
    private GameController gameController;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        gameController = FindObjectOfType<GameController>();
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");

        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    public override void Show() {
        SaveHighscore();
        pauseBackground.disableInteractability = true;
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        moveUI.FadeTo(new Vector2(0f, 40f), 0.5f);
    }

    private void SaveHighscore() {
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;
        highscoreUI.text = Score.instance.highscore.ToString();
        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp);
        EventManager.TriggerEvent("HighscoreEntryAdded");
    }
}
