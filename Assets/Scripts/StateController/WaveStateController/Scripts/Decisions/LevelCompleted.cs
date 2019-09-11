using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/Decisions/LevelCompleted")]
public class LevelCompleted : Decision {

    public override bool Decide(StateController controller) => (controller as WaveStateController).levelCompleted;
}
