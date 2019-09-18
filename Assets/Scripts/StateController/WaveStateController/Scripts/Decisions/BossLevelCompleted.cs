using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/Decisions/BossLevelCompleted")]
public class BossLevelCompleted : Decision {

    public override bool Decide(StateController controller) => (controller as WaveStateController).enemyController.activeEnemies.Count == 0;
}
