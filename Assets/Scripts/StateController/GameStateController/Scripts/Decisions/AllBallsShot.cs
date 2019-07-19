using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/AllBallsShot")]
public class AllBallsShot : Decision {

    public override bool Decide(StateController controller) => (controller as GameStateController).ballController.AllBallsShot;
}
