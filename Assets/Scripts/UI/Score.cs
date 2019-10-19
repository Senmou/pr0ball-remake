using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {

    public static Score instance;

    public TextMeshProUGUI scoreUI;

    public int score;
    public int extraScore;
    public int receivableGoldenPoints;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        EventManager.StartListening("SaveGame", OnSaveGame);

        score = PersistentData.instance.scoreData.score;
        extraScore = PersistentData.instance.scoreData.goldenPoints;
        receivableGoldenPoints = PersistentData.instance.scoreData.receivableGoldenPoints;
        UpdateScore();
    }

    private void OnSaveGame() {
        PersistentData.instance.scoreData.score = score;
        PersistentData.instance.scoreData.goldenPoints = extraScore;
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

    public bool PurchaseExtraUpgrade(int upgradePrice) {
        if (extraScore >= upgradePrice) {
            extraScore -= upgradePrice;
            return true;
        } else
            return false;
    }

    public void IncScore(int amount) {
        score += amount;
        UpdateScore();
    }

    public void BookReceivableGoldenPoints() {
        extraScore += receivableGoldenPoints;
        receivableGoldenPoints = 0;
    }

    public void IncReceivableGoldenPoints(int amount) {
        receivableGoldenPoints += amount;
    }

    private void UpdateScore() {
        scoreUI.text = score.ToStringFormatted();
    }

    public void ResetData() {
        score = 0;
        UpdateScore();
    }
}
