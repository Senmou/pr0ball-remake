using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/AllEnemiesBelowDottedLine")]
public class AllEnemiesBelowDottedLine : Decision {

    public override bool Decide(StateController controller) => (controller as PlayStateController).enemyController.AllEnemiesBelowDottedLine();
}
