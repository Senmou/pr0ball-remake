using UnityEngine;

public class SkillBar : MonoBehaviour {

    public SkillMenu skillMenu;
    public SkillBarSlot[] slots;

    public void ShowMenuOnClick(SkillBarSlot slot) {
        Vector2 menuPos = new Vector2(slot.transform.position.x, -12.25f);
        skillMenu.lastSkillBarSlotClicked = slot;
        skillMenu.DisplaySkills(slot.skills);
        skillMenu.Show(menuPos);
    }
}
