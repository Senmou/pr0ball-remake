using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/Decisions/LevelCompleted")]
public class LevelCompleted : Decision {

    public override bool Decide(StateController controller) {
        WaveStateController c = controller as WaveStateController;

        return c.levelCompleted || c.enemyController.activeEnemies.Count == 0;
    }
}
