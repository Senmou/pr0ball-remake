﻿using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/FinishCycle")]
public class FinishCycle : OneShot {

    public override void Act(StateController controller) {
        PlayStateController c = controller as PlayStateController;
        c.ballController.CancelShooting();
        c.ballController.OnCycleFinish(c);

        LevelData.Wave++;

        if (LevelData.Wave == 1)
            c.reachedNextLevel = true;
        else
            c.reachedNextWave = true;
    }
}
