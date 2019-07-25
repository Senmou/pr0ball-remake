using UnityEngine;

public class SkillMenu : MonoBehaviour {

    public SkillMenuSlot[] slots;

    // This skillBarSlot opened the current menu
    public SkillBarSlot lastSkillBarSlotClicked;

    private PauseBackground pauseBackground;

    private void Awake() {
        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    private void Update() {
        if ((Input.GetMouseButtonDown(0) && !InputHelper.instance.IsPointerOverUIObject()) ||
            (Input.GetMouseButtonUp(0) && !InputHelper.instance.ClickedOnTag("SkillBarSlot"))) {
            Hide();
        }
    }

    public void EquipSkillOnClick(SkillMenuSlot slot) {
        lastSkillBarSlotClicked.EquipSkill(slot.skill);
    }

    public void DisplaySkills(Skill[] newSkillsToDisplay) {
        for (int i = 0; i < 4; i++) {
            slots[i].skill = newSkillsToDisplay[i];
            slots[i].UpdateSlot();
        }
    }

    public void Show(Vector2 pos) {
        pauseBackground.EnableBackground();
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void Hide() {
        pauseBackground.DisableBackground();
        gameObject.SetActive(false);
    }
}
