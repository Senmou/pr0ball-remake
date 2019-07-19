using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/Shooting")]
public class ShootingOneShot : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.ballController.Shoot();
    }
}
