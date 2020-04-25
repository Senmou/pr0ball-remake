using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public static Score instance;

    public HeartContainer[] heartContainers;

    [HideInInspector] public long score;
    [HideInInspector] public long highscore;
    [HideInInspector] public int skillPoints;
    [HideInInspector] public long scoreBackup;
    [HideInInspector] public int lifes;

    private const int maxLifes = 3;
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

        lifes = PersistentData.instance.scoreData.lifes;
        score = PersistentData.instance.scoreData.score;
        highscore = PersistentData.instance.scoreData.highscore;
        skillPoints = PersistentData.instance.scoreData.skillPoints;
        UpdateUI();
    }

    private void Start() {
        scoreBackup = PersistentData.instance.scoreData.score + PersistentData.instance.backupOffset;
        UpdateHeartContainers(withAnimation: false);
    }

    private void OnChacheData() {
        PersistentData.instance.scoreData.lifes = lifes;
        PersistentData.instance.scoreData.score = score;
        PersistentData.instance.scoreData.highscore = highscore;
        PersistentData.instance.scoreData.skillPoints = skillPoints;
    }

    public int GetRewardScoreForClearingLevel() {
        int reward = 50 * (20 - LevelData.Wave);
        IncScore(reward);
        return reward;
    }

    public bool PaySkillPoints(int upgradePrice) {
        if (skillPoints >= upgradePrice) {
            skillPoints -= upgradePrice;
            UpdateUI();
            return true;
        } else
            return false;
    }

    public void GainLife() {
        if (lifes < maxLifes) {
            lifes++;
            UpdateHeartContainers();
        }
    }

    public void LoseLife() {
        if (lifes > 0) {
            lifes--;
            UpdateHeartContainers();
        }

        if (lifes <= 0) {
            playStateController.isGameOver = true;
            PersistentData.instance.isGameOver = true;
        }
    }

    public void UpdateHeartContainers(bool withAnimation = true) {
        for (int i = 0; i < maxLifes; i++) {
            if (i >= maxLifes - lifes) {
                heartContainers[i].Restore();
            } else {
                heartContainers[i].Explode(withAnimation);
            }
        }
    }

    public void IncScore(int amount) {
        score += amount;
        scoreBackup += amount;

        // Anti cheat measurement
        if (scoreBackup - score != PersistentData.instance.backupOffset) {
            DecScore(2 * (long)Mathf.Abs(score));
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
        scoreBackup -= amount;

        // if score was manipulated
        if (scoreBackup - score != PersistentData.instance.backupOffset) {
            highscore = score;
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
        scoreBackup = PersistentData.instance.backupOffset;
        highscore = 0;
        skillPoints = 0;
        lifes = maxLifes;
        UpdateUI();
        UpdateHeartContainers(withAnimation: false);
    }
}
