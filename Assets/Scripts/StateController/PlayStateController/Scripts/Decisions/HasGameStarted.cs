using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/HasGameStarted")]
public class HasGameStarted : Decision {

    public override bool Decide(StateController controller) => (controller as PlayStateController).hasGameStarted;
}
