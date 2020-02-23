using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public static Score instance;

    public TextMeshProUGUI scoreUI;

    public int score;
    public int highscore;
    public int skillPoints;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        EventManager.StartListening("SaveGame", OnSaveGame);
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);

        score = PersistentData.instance.scoreData.score;
        highscore = PersistentData.instance.scoreData.highscore;
        skillPoints = PersistentData.instance.scoreData.skillPoints;
        UpdateScore();
    }

    private void OnReachedNextLevel() {
        IncSkillPoints(1);
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
        UpdateScore();
    }
    
    public void IncSkillPoints(int amount) {
        skillPoints += amount;
    }

    private void UpdateScore() {
        scoreUI.text = score.ToString();

        if (score < 0)
            CanvasManager.instance.SwitchCanvas(CanvasType.GAMEOVER);
    }

    public void ResetData() {
        score = 0;
        highscore = 0;
        UpdateScore();
    }
}
