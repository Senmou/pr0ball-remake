using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/SpawnEnemies")]
public class SpawnEnemies : OneShot {

    public override void Act(StateController controller) {
        PlayStateController c = controller as PlayStateController;

        // failed boss level
        if (c.fightingBoss && c.reachedNextLevel) {
            int remainingEnemies = c.enemyController.activeEnemies.Count;
            c.playerHP.TakeDamage(remainingEnemies);
            c.enemyController.DespawnAllEnemies();
            c.enemyController.CreateInitialWaves();
            c.fightingBoss = false;
            c.reachedBossLevel = false;
            LevelData.LevelFailed();
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

            // cleared level before wave 20 is over
            if (c.enemyController.activeEnemies.Count == 0) {
                if (c.nextLevelIsBoss)
                    StartBossLevel();
                else
                    c.enemyController.CreateInitialWaves();
                LevelData.LevelUp();
            } else {
                c.enemyController.CreateWave();
                return;
            }
        }

        void StartBossLevel() {
            c.fightingBoss = true;
            c.enemyController.DespawnAllEnemies();
            c.enemyController.CreateBossWave();
        }
    }
}
