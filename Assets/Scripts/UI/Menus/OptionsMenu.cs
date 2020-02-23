using UnityEngine;

public class OptionsMenu : CanvasController {

    private MoveUI moveUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
    }
    
    public override void Show() {
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
    }
}
