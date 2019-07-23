using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour {

    // The 4 skills displayed in the menu
    public Skill[] skills;
    public GameObject[] slots;

    private void Update() {
        if ((Input.GetMouseButtonDown(0) && !InputHelper.instance.IsPointerOverUIObject() ||
            Input.GetMouseButtonUp(0) && InputHelper.instance.IsPointerOverUIObject()) &&
            !InputHelper.instance.ClickedOnTag("SkillBarSlot")) {
            Hide();
        }
    }

    public void DisplaySkills(Skill[] newSkillsToDisplay) {
        skills = newSkillsToDisplay;
        UpdateSlots();
    }

    public void Show(Vector2 pos) {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void UpdateSlots() {
        for (int i = 0; i < 4; i++) {
            Image image = slots[i].GetComponent<Image>();
            image.sprite = skills[i].icon;

            TextMeshProUGUI skillName = slots[i].GetComponentInChildren<TextMeshProUGUI>();
            skillName.text = skills[i].name;
        }
    }
}
