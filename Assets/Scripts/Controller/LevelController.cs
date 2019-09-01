using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public GameObject levelUI;
    public TextMeshProUGUI currentLevelUI;

    public int currentLevel;
    public bool upcomingBossLevel;

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
        upcomingBossLevel = currentLevel % 10 == 9;
        UpdateLevelUI();
    }
}
