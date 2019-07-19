using UnityEngine;

public class LevelController : MonoBehaviour {

    #region Singleton
    public static LevelController instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public int currentLevel = 1;

    private void Start() {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        ShowCurrentLevel();
    }

    public void ShowCurrentLevel() {
        Debug.Log("Current Level: " + currentLevel);
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            Debug.Log("pause");
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationFocus(bool focus) {
        if (!focus) {
            if (Application.platform == RuntimePlatform.WindowsEditor) {
                PlayerPrefs.SetInt("CurrentLevel", currentLevel);
                PlayerPrefs.Save();
            }
        }
    }
}
