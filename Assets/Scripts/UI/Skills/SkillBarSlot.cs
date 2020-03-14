using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillBarSlot : MonoBehaviour {

    public Skill skill;
    public Sprite defaultSprite;

    private Image image;
    private Image clockImage;
    private TextMeshProUGUI costUI;

    private void Awake() {
        image = GetComponent<Image>();
        clockImage = transform.FindChild<Image>("Clock");
        costUI = transform.FindChild<TextMeshProUGUI>("Cost");

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

    public void UpdateCostUI(int cost) {
        if (!skill.locked) {
            costUI.text = cost.ToString();
            costUI.gameObject.SetActive(true);
        }
    }

    public void UpdateSlot(Skill skill = null) {

        if (skill == null) {
            image.sprite = defaultSprite;
            return;
        }

        if (skill.locked)
            image.sprite = defaultSprite;
        else
            image.sprite = skill.Icon;
    }
}
