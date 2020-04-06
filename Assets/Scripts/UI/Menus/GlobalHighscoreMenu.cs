using UnityEngine;

public class GlobalHighscoreMenu : CanvasController {

    private MoveUI moveUI;
    private GlobalHighscoreTable table;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        table = FindObjectOfType<GlobalHighscoreTable>();
    }

    public override void Show() {
        table.UpdateUI();
        moveUI.FadeTo(Vector2.zero, 0.5f);
    }

    public override void Hide() {
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
        float screenWidth = edgeVector.x * 2f;

        moveUI.FadeTo(new Vector2(-screenWidth, 0f), 0.5f, true);
    }
}
