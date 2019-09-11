using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/OneShots/SpawnWave")]
public class SpawnWave : OneShot {

    public override void Act(StateController controller) {
        WaveStateController c = controller as WaveStateController;
        c.enemyController.CreateWave();
    }
}
