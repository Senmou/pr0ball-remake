using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public static bool isGamePaused = false;

    private PauseMenu mainMenu;
    private SkillMenu skillMenu;
    private BallMenu ballMenu;
    private EnemyController enemyController;
    private RestartGame restartGame;

    private float elapsedTimeSinceRestart;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        mainMenu = FindObjectOfType<PauseMenu>();
        skillMenu = FindObjectOfType<SkillMenu>();
        ballMenu = FindObjectOfType<BallMenu>();
        enemyController = FindObjectOfType<EnemyController>();
        restartGame = FindObjectOfType<RestartGame>();

        LevelData.SetCurrentLevelData(PersistentData.instance.currentLevelData);

        EventManager.StartListening("SaveGame", OnSaveGame);
        EventManager.StartListening("GameRestarted", OnGameRestarted);
    }

    public int GetPlaytimeMinutes() => (int)elapsedTimeSinceRestart / 60;

    private void OnSaveGame() {
        PersistentData.instance.currentLevelData.wave = LevelData.Wave;
        PersistentData.instance.currentLevelData.level = LevelData.Level;
        PersistentData.instance.elapsedTimeSinceRestart = elapsedTimeSinceRestart;
        Statistics.Instance.OnSaveGame();
    }

    private void OnGameRestarted() {
        elapsedTimeSinceRestart = 0f;
    }

    private void Start() {
        if (PersistentData.instance.isGameOver) {
            restartGame.StartNewGame();
        } else {
            enemyController.LoadEntities();
            Statistics.Instance.OnLoadGame();
        }
    }

    private void Update() {

        elapsedTimeSinceRestart += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (CanvasManager.instance.CurrentActiveCanvasType == CanvasType.NONE)
                CanvasManager.instance.SwitchCanvas(CanvasType.PAUSE);
            else if (CanvasManager.instance.CurrentActiveCanvasType != CanvasType.GAMEOVER)
                CanvasManager.instance.GoOneCanvasBack();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        isGamePaused = true;
        EventManager.TriggerEvent("GamePaused");
    }

    public void ResumeGame() {
        StartCoroutine(ResumeGameAtEndOfFrame());
    }

    private IEnumerator ResumeGameAtEndOfFrame() {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 1f;
        isGamePaused = false;
        EventManager.TriggerEvent("GameResumed");
    }
}
