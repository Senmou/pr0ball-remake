using UnityEngine;

public class SkillBar : MonoBehaviour {

    public SkillMenu skillMenu;

    private SkillBarSlot[] slots;

    private void Awake() {

        slots = new SkillBarSlot[3];
        slots[0] = transform.FindChild<SkillBarSlot>("Slot_1");
        slots[1] = transform.FindChild<SkillBarSlot>("Slot_2");
        slots[2] = transform.FindChild<SkillBarSlot>("Slot_3");
    }

    private void Start() {
        UpdateSlots();
    }

    public void UpdateSlots() {
        for (int i = 0; i < slots.Length; i++) {
            slots[i].UpdateSlot();
        }
    }

    // Used when a long press on the SkillBarSlot is detected
    public void ShowMenuOnLongClick(SkillBarSlot slot) {
        if (!skillMenu.isVisible) {
            skillMenu.DisplaySkills(slot.skill);
            skillMenu.lastSkillBarSlotClicked = slot;
            CanvasManager.instance.SwitchCanvas(CanvasType.SKILLS);
        }
    }

    // Used when SkillBarSlot is tapped while the menu is open
    public void SwitchMenuOnClick(SkillBarSlot slot) {
        if (skillMenu.isVisible) {
            skillMenu.lastSkillBarSlotClicked = slot;
            skillMenu.DisplaySkills(slot.skill);
            skillMenu.Show();
        }
    }

    public void ResetData() {

        for (int i = 0; i < slots.Length; i++) {
            slots[i].skill.ResetData();
        }
        UpdateSlots();
    }
}
