using UnityEngine.UI;
using UnityEngine;

public class SkillBarSlot : MonoBehaviour {

    public Skill skill;
    public Sprite defaultSprite;

    [HideInInspector] public Skill equippedSkill;

    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
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
        if (equippedSkill)
            image.sprite = equippedSkill.Icon;
        else
            image.sprite = defaultSprite;
    }

    public void ResetData() {
        equippedSkill = null;
    }
}
