using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/TappedOnPauseBackground")]
public class TappedOnPauseBackground : Decision {

    public override bool Decide(StateController controller) => (controller as GameStateController).tappedOnPauseBackground;
}
