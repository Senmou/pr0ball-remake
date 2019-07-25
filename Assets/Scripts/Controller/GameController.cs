using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public static bool isGamePaused = false;

    private MainMenu mainMenu;
    private SkillMenu skillMenu;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        mainMenu = FindObjectOfType<MainMenu>();
        skillMenu = FindObjectOfType<SkillMenu>();
    }

    private void Start() {
        skillMenu.Hide();
    }

    private void Update() {
        OnBackButtonPressed();

        if (Input.GetMouseButtonDown(0))
            InputHelper.instance.PrintClickedElementsName();
    }

    private void OnBackButtonPressed() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            mainMenu.Show();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        isGamePaused = true;
        EventManager.TriggerEvent("GamePaused");
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        isGamePaused = false;
        EventManager.TriggerEvent("GameResumed");
    }
}
