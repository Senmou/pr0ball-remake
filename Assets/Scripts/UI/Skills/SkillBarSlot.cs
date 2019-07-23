using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarSlot : MonoBehaviour {

    // There are 16 different skills in total
    // There are 4 groups A, B, C, D with each 4 skills
    // Each slot has one group and can display 1 skill at a time
    public Skill[] skills;
    public Skill equipedSkill;

    private Image image;
    private TextMeshProUGUI skillName;

    private void Awake() {
        image = GetComponent<Image>();
        skillName = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void EquipSkill(Skill skill) {
        equipedSkill = skill;
        UpdateSlot();
    }

    public void UpdateSlot() {
        image.sprite = equipedSkill.icon;
        skillName.text = equipedSkill.name;
    }
}
