using UnityEngine;

public class SkillMenu : MonoBehaviour {

    public SkillMenuSlot[] slots;

    // This skillBarSlot opened the current menu
    public SkillBarSlot lastSkillBarSlotClicked;

    private AudioSource errorSfx;
    private PauseBackground pauseBackground;
    private GameStateController gameStateController;

    private bool isVisible;

    private void Awake() {
        pauseBackground = FindObjectOfType<PauseBackground>();
        gameStateController = FindObjectOfType<GameStateController>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
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

    public void CloseButtonOnClick() {
        gameStateController.skillMenuCloseButtonPressed = true;
    }

    public void Show() {
        GameController.instance.PauseGame();
        pauseBackground.SetBottomMargin(4f);
        gameObject.SetActive(true);
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        pauseBackground.SetBottomMargin(0f);
        gameObject.SetActive(false);
    }
}
