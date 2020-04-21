using System;
using TMPro;

public class GameOverScreen : CanvasController {

    private MoveUI moveUI;
    private TextMeshProUGUI playtimeUI;
    private TextMeshProUGUI highscoreUI;
    private StatisticsMenu statisticsMenu;
    private GameController gameController;
    private PauseBackground pauseBackground;
    private HighscoreController highscoreController;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        statisticsMenu = FindObjectOfType<StatisticsMenu>();
        gameController = FindObjectOfType<GameController>();
        pauseBackground = FindObjectOfType<PauseBackground>();
        highscoreController = FindObjectOfType<HighscoreController>();
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
    }

    public override void Show() {
        pauseBackground.disableInteractability = true;
        LeanTween.moveY(gameObject, 0f, showEaseDuration)
            .setIgnoreTimeScale(true)
            .setEase(showEaseType).setOnComplete(() => {
                if (string.IsNullOrEmpty(PersistentData.instance.playerName) || string.IsNullOrWhiteSpace(PersistentData.instance.playerName)) {
                    CanvasManager.instance.SwitchCanvas(CanvasType.NAME, false);
                }
            });
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
        CanvasManager.instance.SwitchCanvas(CanvasType.STATISTICS);
    }

    public void SaveHighscore() {
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;
        highscoreUI.text = Score.instance.highscore.ToString();
        Statistics stats = Statistics.Instance;
        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp, stats.GetCopy<Statistics>());

        highscoreController.UploadHighscore(Score.instance.highscore);
    }
}
