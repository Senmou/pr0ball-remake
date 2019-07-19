using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public static bool isGamePaused = false;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        OnBackButtonPressed();
    }

    private void OnBackButtonPressed() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(0);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        Debug.Log(scene.name);
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
