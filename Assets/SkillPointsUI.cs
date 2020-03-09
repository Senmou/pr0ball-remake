using UnityEngine;
using TMPro;

public class SkillPointsUI : MonoBehaviour {

    private int oldSkillPointCount;
    private TextMeshProUGUI skillPointCountUI;

    private void Awake() {
        skillPointCountUI = transform.FindChild<TextMeshProUGUI>("Value");
    }

    private void Start() {
        oldSkillPointCount = Score.instance.skillPoints;
        UpdateUI();
    }

    private void Update() {

        if (Input.GetKey(KeyCode.UpArrow))
            Score.instance.IncSkillPoints(1);

        if (oldSkillPointCount != Score.instance.skillPoints) {
            UpdateUI();
            oldSkillPointCount = Score.instance.skillPoints;
        }
    }

    private void UpdateUI() {
        skillPointCountUI.text = Score.instance.skillPoints.ToString();
    }
}
