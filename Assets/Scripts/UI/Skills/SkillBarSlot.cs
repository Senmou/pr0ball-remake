using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarSlot : MonoBehaviour {

    public Skill[] skills;
    public Skill equippedSkill;
    public Sprite defaultSprite;

    private Image image;
    private TextMeshProUGUI coolDownUI;

    private bool showCoolDown;

    private void Awake() {
        image = GetComponent<Image>();
        coolDownUI = transform.FindChild<TextMeshProUGUI>("CoolDownText");
    }

    private void Update() {

        if (equippedSkill)
            showCoolDown = equippedSkill.remainingCoolDown > 0;
        else
            showCoolDown = false;

        coolDownUI.gameObject.SetActive(showCoolDown);
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
        if (equippedSkill) {
            image.sprite = equippedSkill.Icon;
            coolDownUI.text = equippedSkill.remainingCoolDown.ToString();
        } else {
            image.sprite = defaultSprite;
            coolDownUI.gameObject.SetActive(false);
        }
    }

    public void ResetData() {
        equippedSkill = null;
    }
}
