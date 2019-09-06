﻿using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/ShowOptionsMenu")]
public class ShowOptionsMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.optionsMenu.Show();
    }
}
