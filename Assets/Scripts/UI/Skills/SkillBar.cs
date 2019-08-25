using UnityEngine;

public class SkillBar : MonoBehaviour {

    public SkillMenu skillMenu;
    public SkillBarSlot[] slots;

    public void ShowMenuOnClick(SkillBarSlot slot) {
        skillMenu.lastSkillBarSlotClicked = slot;
        skillMenu.DisplaySkills(slot.skills);
        skillMenu.Show();
    }
}
