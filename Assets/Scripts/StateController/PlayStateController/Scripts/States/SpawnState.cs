using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/States/SpawnState")]
public class SpawnState : State {

    protected override void OnExitState(StateController controller) {
        PlayStateController c = controller as PlayStateController;
        c.reachedNextWave = false;
        c.reachedNextLevel = false;
    }
}
