using UnityEngine;

public class SkillBar : MonoBehaviour {

    public SkillMenu skillMenu;

    private SkillBarSlot[] slots;

    private void Awake() {

        slots = new SkillBarSlot[4];
        slots[0] = transform.FindChild<SkillBarSlot>("Slot_1");
        slots[1] = transform.FindChild<SkillBarSlot>("Slot_2");
        slots[2] = transform.FindChild<SkillBarSlot>("Slot_3");
        slots[3] = transform.FindChild<SkillBarSlot>("Slot_4");

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void Start() {
        InitSkillBarSlotsWithLoadedData();
    }

    private void InitSkillBarSlotsWithLoadedData() {
        for (int i = 0; i < slots.Length; i++) {
            int id = PersistentData.instance.skillData.equippedSkillIDs[i];
            if (id == -1 || slots[i] == null)
                continue;
            slots[i].EquipSkill(slots[i].skill);
            slots[i].UpdateSlot();
        }
    }

    private void OnSaveGame() {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i] == null || slots[i].equippedSkill == null)
                continue;
            PersistentData.instance.skillData.equippedSkillIDs[i] = slots[i].equippedSkill.id;
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
            slots[i].ResetData();
            slots[i].UpdateSlot();
        }
    }
}
