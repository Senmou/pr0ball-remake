using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/SpawnEnemies")]
public class SpawnEnemies : OneShot {

    public override void Act(StateController controller) {
        PlayStateController c = controller as PlayStateController;

        CanvasManager.instance.SwitchCanvas(CanvasType.NONE);

        // game restarted
        if (c.gameRestarted) {
            LevelData.ResetData();
            c.enemyController.DespawnAllEnemies();
            c.enemyController.CreateInitialWaves();
            c.gameRestarted = false;
            return;
        }

        if (c.reachedNextLevel) {
            c.enemyController.DespawnAllEnemies();
            c.enemyController.CreateInitialWaves();
            return;
        }

        if (c.reachedNextWave) {

            // cleared level before wave 20 is over OR all enemies are below the dotted line
            if (c.enemyController.activeEnemies.Count == 0 || c.enemyController.AllEnemiesBelowDottedLine()) {
                c.enemyController.DespawnAllEnemies();
                c.enemyController.CreateInitialWaves();
                LevelData.LevelUp();
            } else {
                c.enemyController.CreateWave();
                c.enemyController.CheckForEnemiesWhichReachedDeadline();
                return;
            }

            c.enemyController.CheckForEnemiesWhichReachedDeadline();
        }
    }
}
