using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/CycleFinished")]
public class CycleFinished : Decision {

    public override bool Decide(StateController controller) => (controller as GameStateController).cycleFinished;
}
