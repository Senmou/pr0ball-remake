using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/OneShots/SpawnInitialWaves")]
public class SpawnInitialWaves : OneShot {

    public override void Act(StateController controller) {
        WaveStateController c = controller as WaveStateController;
        c.enemyController.CreateInitialWaves();
    }
}
