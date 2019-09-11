using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/States/FinishWaveState")]
public class FinishWaveState : State {

    protected override void OnExitState(StateController controller) {
        WaveStateController c = controller as WaveStateController;
        c.waveCompleted = false;
        c.levelCompleted = false;
        c.bossLevelUpcoming = false;
    }
}
