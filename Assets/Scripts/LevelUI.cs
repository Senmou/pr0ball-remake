using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour {

    private TextMeshProUGUI levelUI;
    private TextMeshProUGUI dangerLevelUI;
    private TextMeshProUGUI dangerLevelBenitratorUI;
    private DotColorController colorController;

    private void Awake() {
        levelUI = GetComponent<TextMeshProUGUI>();
        colorController = FindObjectOfType<DotColorController>();
        dangerLevelUI = GameObject.Find("DangerLevelValue").GetComponent<TextMeshProUGUI>();
        dangerLevelBenitratorUI = GameObject.Find("DangerLevelBenitrator").GetComponent<TextMeshProUGUI>();
        UpdateDangerLevelUI();
        UpdateLevelUI();
    }

    public void UpdateLevelUI() {
        if (levelUI)
            levelUI.text = LevelData.Level.ToString();
    }

    public void UpdateDangerLevelUI() {
        dangerLevelUI.text = LevelData.DangerLevel.ToString() + "%";
        dangerLevelUI.color = colorController.GetDangerLevelColor(LevelData.DangerLevel);

        dangerLevelBenitratorUI.text = LevelData.DangerLevel.ToString() + "%";
        dangerLevelBenitratorUI.color = colorController.GetDangerLevelColor(LevelData.DangerLevel);
    }
}
