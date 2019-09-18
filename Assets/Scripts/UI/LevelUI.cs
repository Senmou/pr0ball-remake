using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour {

    public TextMeshProUGUI levelUI;

    private void Awake() {
        levelUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        levelUI.text = LevelData.level.ToString();
    }
}
