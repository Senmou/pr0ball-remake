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
    }

    private void Start() {
        skillMenu.Hide();
        ballMenu.Hide();
        enemyController.SpawnInitialWaves();
    }
    
    private void Update() {
        OnBackButtonPressed();

        if (Input.GetMouseButtonDown(0))
            InputHelper.instance.PrintClickedElementsName();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            EventManager.TriggerEvent("WaveCompleted");

        if (Input.GetKeyDown(KeyCode.UpArrow))
            EventManager.TriggerEvent("ReachedNextLevel");

        if (Input.GetKeyDown(KeyCode.Escape))
            gameStateController.backButtonPressed = true;

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
