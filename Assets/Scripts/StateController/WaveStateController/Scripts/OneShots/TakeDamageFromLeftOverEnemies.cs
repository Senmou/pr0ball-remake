using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/OneShots/TakeDamageFromLeftOverEnemies")]
public class TakeDamageFromLeftOverEnemies : OneShot {

    public override void Act(StateController controller) {
        WaveStateController c = controller as WaveStateController;
        int leftOverEnemies = c.enemyController.activeEnemies.Count;
        c.playerHP.TakeDamage(leftOverEnemies);
    }
}
