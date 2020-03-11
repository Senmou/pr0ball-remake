using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/ChacheData")]
public class ChacheData : OneShot {

    public override void Act(StateController controller) {
        Debug.Log("aim");
        EventManager.TriggerEvent("ChacheData");
    }
}
