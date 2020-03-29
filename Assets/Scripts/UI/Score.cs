using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public static Score instance;

    [HideInInspector] public long score;
    [HideInInspector] public long highscore;
    [HideInInspector] public int skillPoints;
    [HideInInspector] public long scoreBackup;

    public long Offset { get => 42; }

    private TextMeshProUGUI scoreUI;
    private PlayStateController playStateController;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        EventManager.StartListening("ChacheData", OnChacheData);

        scoreUI = transform.FindChild<TextMeshProUGUI>("Value");
        playStateController = FindObjectOfType<PlayStateController>();

        score = PersistentData.instance.scoreData.score;
        highscore = PersistentData.instance.scoreData.highscore;
        skillPoints = PersistentData.instance.scoreData.skillPoints;
        scoreBackup = PersistentData.instance.scoreData.score + Offset;
        UpdateUI();
    }

    private void OnChacheData() {
        PersistentData.instance.scoreData.score = score;
        PersistentData.instance.scoreData.highscore = highscore;
        PersistentData.instance.scoreData.skillPoints = skillPoints;
    }

    public bool PaySkillPoints(int upgradePrice) {
        if (skillPoints >= upgradePrice) {
            skillPoints -= upgradePrice;
            UpdateUI();
            return true;
        } else
            return false;
    }

    public void IncScore(int amount) {
        score += amount;
        scoreBackup += amount;

        // Anti cheat measurement
        if (scoreBackup - score != Offset) {
            DecScore(2 * (long)Mathf.Abs(score) + 15051505);
            highscore = score;
        }

        if (score > highscore) {
            highscore = score;
        }

        UpdateUI();
    }

    public void DecScore(long amount) {

        if (score < 0)
            return;

        score -= amount;

        if (score < 0) {
            playStateController.isGameOver = true;
            PersistentData.instance.isGameOver = true;
        }

        UpdateUI();
    }

    public void IncSkillPoints(int amount) {
        skillPoints += amount;
    }

    public void UpdateUI() {
        scoreUI.text = score.ToString();
    }

    public void ResetData() {
        score = 0;
        highscore = 0;
        skillPoints = 0;
        UpdateUI();
    }
}
