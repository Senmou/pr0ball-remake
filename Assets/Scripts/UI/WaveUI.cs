using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour {

    private TextMeshProUGUI waveUI;

    private void Awake() {
        waveUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        waveUI.text = LevelData.Wave.ToString() + "/" + LevelData.wavesPerLevel.ToString();
    }
}
