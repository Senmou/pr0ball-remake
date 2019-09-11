using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/Decisions/WaveCompleted")]
public class WaveCompleted : Decision {

    public override bool Decide(StateController controller) => (controller as WaveStateController).waveCompleted;
}
