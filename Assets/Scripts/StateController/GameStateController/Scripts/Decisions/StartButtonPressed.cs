using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/StartButtonPressed")]
public class StartButtonPressed : Decision {

    public override bool Decide(StateController controller) => (controller as GameStateController).startButton.pressed;
}
