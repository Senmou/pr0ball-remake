using UnityEngine;

public class SkillBar : MonoBehaviour {

    public SkillMenu skillMenu;

    private SkillBarSlot[] slots;
    private GameStateController gameStateController;

    private void Awake() {
        gameStateController = FindObjectOfType<GameStateController>();

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
            // every slot holds 4 skills
            // the skill id's are 0-15, therefore mod 4
            slots[i].EquipSkill(slots[i].skills[id % 4]);
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
            skillMenu.DisplaySkills(slot.skills);
            skillMenu.lastSkillBarSlotClicked = slot;
            gameStateController.skillBarLongClick = true;
        }
    }

    // Used when SkillBarSlot is tapped while the menu is open
    public void SwitchMenuOnClick(SkillBarSlot slot) {
        if (skillMenu.isVisible) {
            skillMenu.lastSkillBarSlotClicked = slot;
            skillMenu.DisplaySkills(slot.skills);
            skillMenu.Show();
        }
    }
}
