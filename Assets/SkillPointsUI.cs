using UnityEngine.UI;
using UnityEngine;

public class SkillPointsUI : MonoBehaviour {

    private Image[] images;
    private int oldSkillPointCount;

    private void Awake() {
        images = GetComponentsInChildren<Image>();
    }

    private void Start() {
        oldSkillPointCount = Score.instance.skillPoints;
        UpdateUI();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.UpArrow))
            Score.instance.IncSkillPoints(1);

        if (oldSkillPointCount != Score.instance.skillPoints) {
            UpdateUI();
            oldSkillPointCount = Score.instance.skillPoints;
        }
    }

    private void UpdateUI() {
        int skillPointCount = Score.instance.skillPoints;

        for (int i = 0; i < skillPointCount; i++) {
            images[i].color = new Color(0.8f, 0.8f, 0.8f, 1f);
        }

        for (int i = 2; i >= skillPointCount; i--) {
            images[i].color = new Color(0.2f, 0.2f, 0.2f, 1f);
        }
    }
}
