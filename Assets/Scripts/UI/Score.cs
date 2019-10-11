using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {

    public static Score instance;

    public TextMeshProUGUI scoreUI;

    public int score;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        EventManager.StartListening("SaveGame", OnSaveGame);

        score = PersistentData.instance.scoreData.score;
        UpdateScore();
    }

    private void OnSaveGame() {
        PersistentData.instance.scoreData.score = score;
    }

    public bool PurchaseUpgrade(int upgradePrice) {
        if (score >= upgradePrice) {
            score -= upgradePrice;
            UpdateScore();
            return true;
        } else
            return false;
    }

    public void IncScore(int amount) {
        score += amount;
        UpdateScore();
    }

    private void UpdateScore() {
        scoreUI.text = score.ToString();
    }
}
