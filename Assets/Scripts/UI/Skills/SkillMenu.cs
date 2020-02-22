using TMPro;
using UnityEngine;

public class SkillMenu : MonoBehaviour {

    public SkillMenuSlot slot;

    // This skillBarSlot opened the current menu
    public SkillBarSlot lastSkillBarSlotClicked;

    private MoveUI moveUI;
    private AudioSource errorSfx;
    private PauseBackground pauseBackground;
    private GameStateController gameStateController;

    public bool isVisible;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        pauseBackground = FindObjectOfType<PauseBackground>();
        gameStateController = FindObjectOfType<GameStateController>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
    }

    public void DisplaySkills(Skill newSkillToDisplay) {
        slot.skill = newSkillToDisplay;
        slot.UpdateSlot();
    }

    public void CloseButtonOnClick() {
        gameStateController.skillMenuCloseButtonPressed = true;
    }

    public void Show() {
        isVisible = true;
        GameController.instance.PauseGame();
        pauseBackground.SetBottomMargin(4f);
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public void Hide() {
        isVisible = false;
        GameController.instance.ResumeGame();
        pauseBackground.SetBottomMargin(0f);
        moveUI.FadeTo(new Vector2(0f, -50f), 0.5f);
    }
}
