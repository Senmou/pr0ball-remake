using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/GameRestarted")]
public class GameRestarted : Decision {

    public override bool Decide(StateController controller) => (controller as PlayStateController).gameRestarted;
}
