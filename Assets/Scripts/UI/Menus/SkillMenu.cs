using UnityEngine;

public class SkillMenu : CanvasController {

    public SkillMenuSlot slot;

    // This skillBarSlot opened the current menu
    public SkillBarSlot lastSkillBarSlotClicked;

    private MoveUI moveUI;
    private AudioSource errorSfx;
    private PauseBackground pauseBackground;

    public bool isVisible;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        pauseBackground = FindObjectOfType<PauseBackground>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
    }

    public void DisplaySkills(Skill newSkillToDisplay) {
        slot.skill = newSkillToDisplay;
        slot.UpdateSlot();
    }

    public override void Show() {
        isVisible = true;
        pauseBackground.SetBottomMargin(4f);
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        isVisible = false;
        pauseBackground.SetBottomMargin(0f);
        moveUI.FadeTo(new Vector2(0f, -50f), 0.5f, true);
    }
}
