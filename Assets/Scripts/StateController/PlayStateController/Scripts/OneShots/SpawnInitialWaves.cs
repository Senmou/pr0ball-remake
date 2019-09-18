using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/SpawnInitialWaves")]
public class SpawnInitialWaves : OneShot {

    public override void Act(StateController controller) {
        PlayStateController c = controller as PlayStateController;
        c.enemyController.CreateInitialWaves();
    }
}
