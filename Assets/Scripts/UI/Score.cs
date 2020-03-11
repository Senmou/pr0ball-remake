using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public static Score instance;

    [HideInInspector] public long score;
    [HideInInspector] public long highscore;
    [HideInInspector] public int skillPoints;

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
        UpdateUI();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.H))
            DecScore(100);

        if (Input.GetKey(KeyCode.J))
            IncScore(100);
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
