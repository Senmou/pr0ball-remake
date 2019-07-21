using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/Shooting")]
public class ShootingOneShot : OneShot {

    public override void Act(StateController controller) {
        PlayStateController c = controller as PlayStateController;
        c.ballController.Shoot();
    }
}
