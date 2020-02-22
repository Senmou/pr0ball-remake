using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public static bool isGamePaused = false;

    private PauseMenu mainMenu;
    private SkillMenu skillMenu;
    private BallMenu ballMenu;
    private EnemyController enemyController;
    private GameStateController gameStateController;

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
        gameStateController = FindObjectOfType<GameStateController>();

        LevelData.SetCurrentLevelData(PersistentData.instance.currentLevelData);

        EventManager.StartListening("SaveGame", OnSaveGame);
        EventManager.StartListening("GameRestarted", OnGameRestarted);
    }

    public int GetPlaytimeMinutes() => (int)elapsedTimeSinceRestart / 60;

    private void OnSaveGame() {
        PersistentData.instance.currentLevelData.level = LevelData.Level;
        PersistentData.instance.elapsedTimeSinceRestart = elapsedTimeSinceRestart;
    }

    private void OnGameRestarted() {
        elapsedTimeSinceRestart = 0f;
    }

    private void Start() {
        ballMenu.Hide();
        skillMenu.Hide();

        enemyController.CreateInitialWaves();
    }

    private void Update() {
        OnBackButtonPressed();

        elapsedTimeSinceRestart += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
            gameStateController.backButtonPressed = true;

        if (Input.GetKey(KeyCode.Space))
            Time.timeScale = 15f;
        else
            Time.timeScale = 1f;
    }

    private void OnBackButtonPressed() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            EventManager.TriggerEvent("BackButtonPressed");
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
