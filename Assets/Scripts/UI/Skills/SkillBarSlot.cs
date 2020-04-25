using UnityEngine.UI;
using UnityEngine;

public class SkillBarSlot : MonoBehaviour {

    public Skill skill;
    public Sprite defaultSprite;
    public Color32 disabledColor;

    private Image icon;
    private SkillController skillController;
    private ItemTween ring;

    private void Awake() {
        icon = transform.FindChild<Image>("Icon");
        ring = GetComponentInChildren<ItemTween>();
        skillController = FindObjectOfType<SkillController>();
    }

    private void Start() {
        if (!skill.hasToken) {
            ring.ScaleToZero(withAnimation: false);
        }
    }

    public void UseSkill() {
        if (!skill.locked)
            skill.UseSkill();
    }

    public void UpdateSlot() {

        if (!skill.hasToken) {
            icon.color = disabledColor;
            ring.ScaleToZero();
        }
        else {
            icon.color = new Color(1f, 1f, 1f, 1f);
            ring.Grow();
        }

        icon.sprite = skill.Icon;
    }
}
