
using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/HasPlayerShot")]
public class HasPlayerShot : Decision {

    public override bool Decide(StateController controller) => Input.GetMouseButtonUp(0);
}
