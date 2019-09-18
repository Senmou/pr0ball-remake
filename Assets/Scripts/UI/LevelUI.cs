using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour {

    public TextMeshProUGUI currentLevelUI;
    private WaveStateController waveStateController;

    private void Awake() {
        waveStateController = FindObjectOfType<WaveStateController>();
    }

    private void Update() {
        UpdateUI();
    }

    private void UpdateUI() {
        currentLevelUI.text = LevelData.level.ToString();
    }
}
