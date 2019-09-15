using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/OneShots/DespawnBoss")]
public class DespawnBoss : OneShot {

    public override void Act(StateController controller) {
        WaveStateController c = controller as WaveStateController;
        int remainingEnemies = c.enemyController.DespawnBoss();
        c.playerHP.TakeDamage(remainingEnemies);
    }
}
