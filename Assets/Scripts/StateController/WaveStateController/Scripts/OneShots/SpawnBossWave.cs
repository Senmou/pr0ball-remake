using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/OneShots/SpawnBossWave")]
public class SpawnBossWave : OneShot {

    public override void Act(StateController controller) {
        WaveStateController c = controller as WaveStateController;
        c.enemyController.CreateBossWave();
    }
}
