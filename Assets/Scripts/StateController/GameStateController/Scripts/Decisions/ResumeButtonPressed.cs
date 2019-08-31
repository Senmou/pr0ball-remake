using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/ResumeButtonPressed")]
public class ResumeButtonPressed : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;

        if (c.resumeButtonPressed) {
            c.resumeButtonPressed = false;
            return true;
        }
        return false;
    }
}
