using System;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {

    private MoveUI moveUI;
    private TextMeshProUGUI playtimeUI;
    private TextMeshProUGUI highscoreUI;
    private GameStateController gameStateController;
    private GameController gameController;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        gameController = FindObjectOfType<GameController>();
        gameStateController = FindObjectOfType<GameStateController>();
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            gameStateController.isGameOver = true;
        }
    }

    public void Show() {
        GameController.instance.PauseGame();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
        UpdateUI();
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        moveUI.FadeTo(new Vector2(0f, 40f), 0.5f);
    }

    private void UpdateUI() {
        highscoreUI.text = Score.instance.highscore.ToString();
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;

        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp);
    }
}
