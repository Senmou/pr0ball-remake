using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarSlot : MonoBehaviour {

    public TextMeshProUGUI coolDown;

    public Skill[] skills;
    public Skill equippedSkill;

    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Update() {
        if (equippedSkill && equippedSkill.remainingCoolDown == 0)
            coolDown.gameObject.SetActive(false);
        else
            coolDown.gameObject.SetActive(true);
    }

    public void UseSkill() {
        equippedSkill?.UseSkill();
    }

    public void EquipSkill(Skill skill) {
        equippedSkill = skill;
        equippedSkill.barSlot = this;
        UpdateSlot();
    }

    public void UpdateSlot() {
        image.sprite = equippedSkill.Icon;
        coolDown.text = equippedSkill.remainingCoolDown.ToString();
    }
}
