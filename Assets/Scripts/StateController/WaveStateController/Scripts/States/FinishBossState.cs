using UnityEngine;

[CreateAssetMenu(menuName = "WaveStateController/States/FinishBossState")]
public class FinishBossState : State {

    protected override void OnExitState(StateController controller) {
        WaveStateController c = controller as WaveStateController;
        c.OnBossCompleted();
    }
}
