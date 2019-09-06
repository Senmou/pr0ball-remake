using TMPro;
using UnityEngine;

public class SkillMenuUnlockButton : MonoBehaviour {

    private TextMeshProUGUI priceTag;

    private void Awake() {
        priceTag = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(int unlockLevel) {
        priceTag.text = "Unlock at level: " + unlockLevel.ToString();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }
}
