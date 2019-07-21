using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/AllBallsShot")]
public class AllBallsShot : Decision {

    public override bool Decide(StateController controller) => (controller as PlayStateController).ballController.AllBallsShot;
}
