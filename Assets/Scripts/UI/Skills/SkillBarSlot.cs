using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarSlot : MonoBehaviour {

    public TextMeshProUGUI skillName;
    public TextMeshProUGUI coolDown;

    // There are 16 different skills in total
    // There are 4 groups A, B, C, D with each 4 skills
    // Each slot has one group and can display 1 skill at a time
    public Skill[] skills;
    public Skill equipedSkill;

    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Update() {
        if (equipedSkill && equipedSkill.coolDownCounter == 0)
            coolDown.gameObject.SetActive(false);
        else
            coolDown.gameObject.SetActive(true);
    }

    public void UseSkill() {
        equipedSkill?.UseSkill();
    }

    public void EquipSkill(Skill skill) {
        equipedSkill = skill;
        equipedSkill.slot = this;
        UpdateSlot();
    }

    public void UpdateSlot() {
        image.sprite = equipedSkill.icon;
        skillName.text = equipedSkill.name;
        coolDown.text = equipedSkill.coolDownCounter.ToString();
    }
}
