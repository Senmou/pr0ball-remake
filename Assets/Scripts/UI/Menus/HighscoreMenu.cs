using UnityEngine;

public class HighscoreMenu : CanvasController {

    private MoveUI moveUI;
    private HighscoreTable highscoreTable;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        highscoreTable = transform.FindChild<HighscoreTable>("HighscoreTable");
    }

    public override void Show() {
        highscoreTable.UpdateUI();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(-40f, 0f), 0.5f, true);
    }
}
