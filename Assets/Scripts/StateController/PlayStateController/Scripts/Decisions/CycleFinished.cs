using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/CycleFinished")]
public class CycleFinished : Decision {

    public override bool Decide(StateController controller) => (controller as PlayStateController).cycleFinished;
}
