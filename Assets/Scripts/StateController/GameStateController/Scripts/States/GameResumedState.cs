using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/States/GameResumedState")]
public class GameResumedState : State {

    protected override void OnExitState(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.isGameOver = false;
        c.backButtonPressed = false;
        c.skillBarLongClick = false;
        c.resumeButtonPressed = false;
        c.newGameButtonPressed = false;
        c.optionsButtonPressed = false;
        c.ballMenuButtonPressed = false;
        c.tappedOnPauseBackground = false;
        c.ballMenuCloseButtonPressed = false;
        c.skillMenuCloseButtonPressed = false;
        c.optionsMenuCloseButtonPressed = false;
    }
}
