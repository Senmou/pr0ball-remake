using UnityEngine;

public class DataPrivacyMenu : CanvasController {

    private MoveUI moveUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
    }

    public override void Show() {
        moveUI.FadeTo(Vector2.zero, 0.5f);
    }

    public override void Hide() {
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
        float screenWidth = edgeVector.x * 2f;

        moveUI.FadeTo(new Vector2(-screenWidth, 0f), 0.5f, true);
    }
}
