using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/OneShots/DespawnAllEnemies")]
public class DespawnAllEnemies : OneShot {

    public override void Act(StateController controller) => (controller as WaveStateController).enemyController.DespawnAllEnemies();
}
