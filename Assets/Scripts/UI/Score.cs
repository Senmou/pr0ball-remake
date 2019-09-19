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

        score = PersistentData.instance.scoreData.score;
        UpdateScore();
    }

    private void Start() {
        score = 0;
    }

    public bool BuySkill(Skill skill) {
        if (score >= skill.price) {
            score -= skill.price;
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
        PersistentData.instance.scoreData.score = score;
    }
}
