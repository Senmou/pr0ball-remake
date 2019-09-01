using UnityEngine;

public class SkillBar : MonoBehaviour {

    public SkillMenu skillMenu;
    public SkillBarSlot[] slots;

    private GameStateController gameStateController;

    private void Awake() {
        gameStateController = FindObjectOfType<GameStateController>();
    }

    // Used when a long press on the SkillBarSlot is detected
    public void ShowMenuOnLongClick(SkillBarSlot slot) {
        skillMenu.DisplaySkills(slot.skills);
        skillMenu.lastSkillBarSlotClicked = slot;
        gameStateController.skillBarLongClick = true;
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
