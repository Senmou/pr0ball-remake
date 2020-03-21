using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillMenuUnlockButton : MonoBehaviour {

    private Image background;
    private TextMeshProUGUI priceTag;

    private void Awake() {
        background = GetComponent<Image>();
        priceTag = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(int unlockLevel) {
        priceTag.text = "Ab Level: " + unlockLevel.ToString();
    }

    public void SetColor(int unlockLevel) {
        if (LevelData.Level < unlockLevel)
            background.color = new Color(0.35f, 0.35f, 0.35f, 1f); // grey
        else
            background.color = new Color(0.8235295f, 0.2352941f, 0.1333333f, 1f); // red
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }
}
