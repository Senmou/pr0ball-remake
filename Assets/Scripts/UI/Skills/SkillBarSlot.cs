using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarSlot : MonoBehaviour {

    public TextMeshProUGUI coolDown;

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
        equipedSkill.barSlot = this;
        UpdateSlot();
    }

    public void UpdateSlot() {
        image.sprite = equipedSkill.Icon;
        coolDown.text = equipedSkill.coolDownCounter.ToString();
    }
}
