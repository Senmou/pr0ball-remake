using UnityEngine.UI;
using UnityEngine;

public class SkillBarSlot : MonoBehaviour {

    public Skill skill;
    public Sprite defaultSprite;

    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void UseSkill() {
        if (!skill.locked)
            skill.UseSkill();
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
