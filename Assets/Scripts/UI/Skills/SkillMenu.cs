using UnityEngine;

public class SkillMenu : MonoBehaviour {

    public SkillMenuSlot[] slots;

    // This skillBarSlot opened the current menu
    public SkillBarSlot lastSkillBarSlotClicked;

    private AudioSource errorSfx;
    private PauseBackground pauseBackground;

    private void Awake() {
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
        pauseBackground = FindObjectOfType<PauseBackground>();
    }
    
    public void EquipSkillOnClick(SkillMenuSlot slot) {
        if (!slot.skill.locked)
            lastSkillBarSlotClicked.EquipSkill(slot.skill);
        else
            errorSfx.Play();
    }
    
    public void DisplaySkills(Skill[] newSkillsToDisplay) {
        for (int i = 0; i < 4; i++) {
            slots[i].skill = newSkillsToDisplay[i];
            slots[i].UpdateSlot();
        }
    }

    public void Show() {
        GameController.instance.PauseGame();
        gameObject.SetActive(true);
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        gameObject.SetActive(false);
    }
}
