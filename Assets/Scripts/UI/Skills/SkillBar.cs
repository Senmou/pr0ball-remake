using UnityEngine;

public class SkillBar : MonoBehaviour {

    public SkillMenu skillMenu;
    public SkillBarSlot[] slots;

    // Used when a long press on the SkillBarSlot is detected
    public void ShowMenuOnClick(SkillBarSlot slot) {
        skillMenu.lastSkillBarSlotClicked = slot;
        skillMenu.DisplaySkills(slot.skills);
        skillMenu.Show();
    }

    // Used when SkillBarSlot is tapped while the menu is open
    public void SwitchMenuOnClick(SkillBarSlot slot) {

        if (skillMenu.gameObject.activeSelf) {
            skillMenu.lastSkillBarSlotClicked = slot;
            skillMenu.DisplaySkills(slot.skills);
            skillMenu.Show();
        }
    }
}
