using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/Decisions/BossLevelUpcoming")]
public class BossLevelUpcoming : Decision {

    public override bool Decide(StateController controller) => (controller as WaveStateController).bossLevelUpcoming;
}
