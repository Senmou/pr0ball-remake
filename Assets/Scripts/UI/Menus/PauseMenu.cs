using UnityEngine;

public class PauseMenu : CanvasController {

    [HideInInspector] public bool visible;

    private MoveUI moveUI;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        pauseBackground = FindObjectOfType<PauseBackground>();

        visible = false;
    }

    public override void Hide() {
        if (visible) {
            pauseBackground.disableInteractability = false;
            visible = false;
            moveUI.FadeTo(new Vector2(30f, 0f), 0.5f);
        }
    }

    public override void Show() {
        if (!visible) {
            pauseBackground.disableInteractability = true;
            visible = true;
            moveUI.FadeTo(Vector2.zero, 0.5f);
        }
    }
}
