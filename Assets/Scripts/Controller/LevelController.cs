using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public GameObject levelUI;
    public TextMeshProUGUI currentLevelUI;

    public int currentLevel;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);
    }

    private void Start() {
        UpdateLevelUI();
    }

    private void UpdateLevelUI() {
        currentLevelUI.text = currentLevel.ToString();
    }

    public void DecreaseLevel(int value) {
        currentLevel -= value;
    }

    private void OnReachedNextLevel() {
        currentLevel++;
        bool isBossLevel = currentLevel % 10 == 0;
        if (isBossLevel) {
            EventManager.TriggerEvent("ReachedBossLevel");
        }
        UpdateLevelUI();
    }
}
