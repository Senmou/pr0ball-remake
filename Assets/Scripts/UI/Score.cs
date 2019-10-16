using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {

    public static Score instance;

    public TextMeshProUGUI scoreUI;

    public int score;
    public int goldenPoints;
    public int receivableGoldenPoints;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        EventManager.StartListening("SaveGame", OnSaveGame);

        score = PersistentData.instance.scoreData.score;
        goldenPoints = PersistentData.instance.scoreData.goldenPoints;
        receivableGoldenPoints = PersistentData.instance.scoreData.receivableGoldenPoints;
        UpdateScore();
    }

    private void OnSaveGame() {
        PersistentData.instance.scoreData.score = score;
        PersistentData.instance.scoreData.goldenPoints = goldenPoints;
        PersistentData.instance.scoreData.receivableGoldenPoints = receivableGoldenPoints;
    }

    public bool PurchaseUpgrade(int upgradePrice) {
        if (score >= upgradePrice) {
            score -= upgradePrice;
            UpdateScore();
            return true;
        } else
            return false;
    }

    public bool PurchaseGoldenUpgrade(int upgradePrice) {
        if (goldenPoints >= upgradePrice) {
            goldenPoints -= upgradePrice;
            return true;
        } else
            return false;
    }

    public void IncScore(int amount) {
        score += amount;
        UpdateScore();
    }

    public void BookReceivableGoldenPoints() {
        goldenPoints += receivableGoldenPoints;
        receivableGoldenPoints = 0;
    }

    public void IncReceivableGoldenPoints(int amount) {
        receivableGoldenPoints += amount;
    }

    private void UpdateScore() {
        scoreUI.text = score.ToString();
    }

    public void ResetData() {
        score = 0;
        UpdateScore();
    }
}
