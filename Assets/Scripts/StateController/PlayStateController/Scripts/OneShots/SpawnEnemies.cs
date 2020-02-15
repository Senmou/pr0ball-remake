using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/SpawnEnemies")]
public class SpawnEnemies : OneShot {

    public override void Act(StateController controller) {
        PlayStateController c = controller as PlayStateController;

        // game restarted
        if (c.gameRestarted) {
            LevelData.ResetData();
            c.enemyController.DespawnAllEnemies();
            c.enemyController.CreateInitialWaves();
            c.gameRestarted = false;
            return;
        }

        // failed boss level
        if (c.fightingBoss && c.reachedNextLevel) {
            int remainingEnemies = c.enemyController.activeEnemies.Count;
            c.enemyController.DespawnAllEnemies();
            c.enemyController.CreateInitialWaves();
            c.fightingBoss = false;
            c.reachedBossLevel = false;
            LevelData.BossLevelFailed();
            return;
        }

        // finished boss level successfully
        if (c.fightingBoss && c.reachedNextWave && c.enemyController.activeEnemies.Count == 0) {
            c.fightingBoss = false;
            c.reachedBossLevel = false;
            LevelData.LevelUp();
            LevelData.Wave = 1;
            c.enemyController.CreateInitialWaves();
            return;
        }

        if (c.fightingBoss)
            return;

        if (c.reachedBossLevel) {
            StartBossLevel();
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
                if (c.nextLevelIsBoss)
                    StartBossLevel();
                else {
                    c.enemyController.DespawnAllEnemies();
                    c.enemyController.CreateInitialWaves();
                }
                LevelData.LevelUp();
            } else {
                c.enemyController.CreateWave();
                c.enemyController.CheckForEnemiesWhichReachedDeadline();
                // at least one enemy reached the deadline; level restart
                //if (c.enemyController.EnemyReachedDeadline()) {
                //c.enemyController.DespawnAllEnemies();
                //c.enemyController.CreateInitialWaves();
                //LevelData.Wave = 1;
                //}

                return;
            }
        }

        void StartBossLevel() {
            c.fightingBoss = true;
            c.enemyController.DespawnAllEnemies();
            c.enemyController.CreateInitialWaves(isBossWave: true);
        }
    }
}
