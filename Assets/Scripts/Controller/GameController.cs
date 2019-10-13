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
    }

    private void OnSaveGame() {
        PersistentData.instance.currentLevelData.wave = LevelData.Wave;
        PersistentData.instance.currentLevelData.level = LevelData.Level;
    }

    private void Start() {
        ballMenu.Hide();
        skillMenu.Hide();
        enemyController.CreateInitialWaves();
    }

    private void Update() {
        OnBackButtonPressed();

        if (Input.GetMouseButtonDown(0))
            InputHelper.instance.PrintClickedElementsName();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            LevelData.Wave++;

        if (Input.GetKey(KeyCode.UpArrow))
            LevelData.Wave++;

        if (Input.GetKeyDown(KeyCode.Escape))
            gameStateController.backButtonPressed = true;

        if (Input.GetKeyDown(KeyCode.Space))
            Time.timeScale = 0.2f;

        if (Input.GetKeyUp(KeyCode.Space))
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
