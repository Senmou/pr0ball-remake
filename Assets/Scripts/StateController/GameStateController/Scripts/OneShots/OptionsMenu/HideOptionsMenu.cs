using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/HideOptionsMenu")]
public class HideOptionsMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;

        OptionsMenu optionsMenu = FindObjectOfType<OptionsMenu>();
        optionsMenu.Hide();
    }
}
