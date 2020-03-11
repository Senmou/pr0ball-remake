using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/ChacheData")]
public class ChacheData : OneShot {

    public override void Act(StateController controller) {
        EventManager.TriggerEvent("ChacheData");
    }
}
