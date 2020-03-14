using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillBarSlot : MonoBehaviour {

    public Skill skill;
    public Sprite defaultSprite;

    private Image image;
    private Image clockImage;
    private TextMeshProUGUI costUI;
    private SkillController skillController;

    private void Awake() {
        image = GetComponent<Image>();
        clockImage = transform.FindChild<Image>("Clock");
        costUI = transform.FindChild<TextMeshProUGUI>("Cost");
        skillController = FindObjectOfType<SkillController>();
        clockImage.gameObject.SetActive(false);
        costUI.gameObject.SetActive(false);
    }

    public void UseSkill() {
        if (!skill.locked)
            skill.UseSkill();
    }

    public void ShowClockImage(bool state) {
        clockImage.gameObject.SetActive(state);
    }

    public void UpdateSlot() {

        if (skill == null) {
            image.sprite = defaultSprite;
            return;
        }

        if (!skill.locked) {
            costUI.text = skill.cost.ToString();
            costUI.gameObject.SetActive(true);
        }

        if (skill.locked)
            image.sprite = defaultSprite;
        else
            image.sprite = skill.Icon;

        ShowClockImage(skill.usedThisTurn);
    }
}
