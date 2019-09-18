using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour {

    public TextMeshProUGUI waveUI;

    private void Awake() {
        waveUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        waveUI.text = LevelData.wave.ToString() + "/" + LevelData.wavesPerLevel.ToString();
    }
}
