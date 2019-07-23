using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour {

    public SkillMenu skillMenu;
    public SkillBarSlot[] slots;
    public Skill[] equipedSkills;

    public void ShowMenuOnClick(SkillBarSlot slot) {
        Vector2 menuPos = new Vector2(slot.transform.position.x, -12.25f);
        skillMenu.DisplaySkills(slot.skills);
        skillMenu.Show(menuPos);
    }
    
    private void UpdateSlots() {
        for (int i = 0; i < 4; i++) {
            UpdateSlot(i);
        }
    }

    private void UpdateSlot(int id) {
            Image image = slots[id].GetComponent<Image>();
            image.sprite = equipedSkills[id].icon;

            TextMeshProUGUI skillName = slots[id].GetComponentInChildren<TextMeshProUGUI>();
            skillName.text = equipedSkills[id].name;
    }
}
