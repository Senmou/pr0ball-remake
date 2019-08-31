using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/AllEnemiesDead")]
public class AllEnemiesDead : Decision {

    public override bool Decide(StateController controller) => (controller as PlayStateController).enemyCount == 0;
}
