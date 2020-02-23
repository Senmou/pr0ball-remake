using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour {

    private TextMeshProUGUI levelUI;

    private void Awake() {
        levelUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        levelUI.text = LevelData.Level.ToString();
    }
}
