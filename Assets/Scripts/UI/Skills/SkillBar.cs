using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour {

    public SkillBase[] equipedSkills;
    public GameObject[] slots;

    public void SetSkill(int skillBarSlotId, SkillBase skill) {
        equipedSkills[skillBarSlotId] = skill;
        UpdateSlot(skillBarSlotId);
    }

    private void UpdateSlot(int slotId) {
            Image image = slots[slotId].GetComponent<Image>();
            image.sprite = equipedSkills[slotId].icon;

            TextMeshProUGUI skillName = slots[slotId].GetComponentInChildren<TextMeshProUGUI>();
            skillName.text = equipedSkills[slotId].name;
    }
}
