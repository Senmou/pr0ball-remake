using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/BackButtonPressed")]
public class BackButtonPressed : Decision {

    public override bool Decide(StateController controller) => (controller as GameStateController).backButtonPressed;
}
