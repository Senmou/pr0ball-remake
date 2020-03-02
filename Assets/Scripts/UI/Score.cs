using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public static Score instance;

    public TextMeshProUGUI scoreUI;

    public int score;
    public int highscore;
    public int skillPoints;
    public int maxSkillPoints = 3;

    private PlayStateController playStateController;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        EventManager.StartListening("SaveGame", OnSaveGame);

        playStateController = FindObjectOfType<PlayStateController>();

        score = PersistentData.instance.scoreData.score;
        highscore = PersistentData.instance.scoreData.highscore;
        skillPoints = PersistentData.instance.scoreData.skillPoints;
        UpdateUI();
    }

    private void OnSaveGame() {
        PersistentData.instance.scoreData.score = score;
        PersistentData.instance.scoreData.highscore = highscore;
        PersistentData.instance.scoreData.skillPoints = skillPoints;
    }

    public bool PaySkillPoints(int upgradePrice) {
        if (skillPoints >= upgradePrice) {
            skillPoints -= upgradePrice;
            return true;
        } else
            return false;
    }

    public void IncScore(int amount) {
        score += amount;
        if (score > highscore)
            highscore = score;

        UpdateUI();
    }

    public void DecScore(int amount) {

        if (score < 0)
            return;

        score -= amount;

        if(score < 0) {
            playStateController.isGameOver = true;
            PersistentData.instance.isGameOver = true;
        }

        UpdateUI();
    }

    public void IncSkillPoints(int amount) {
        skillPoints += amount;
        skillPoints = (skillPoints > maxSkillPoints) ? maxSkillPoints : skillPoints;
    }

    private void UpdateUI() {
        scoreUI.text = score.ToString();
    }

    public void ResetData() {
        score = 0;
        highscore = 0;
        UpdateUI();
    }
}
