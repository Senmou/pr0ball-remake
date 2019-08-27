using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/HasPlayerShot")]
public class HasPlayerShot : Decision {

    public override bool Decide(StateController controller) {
        PlayStateController c = controller as PlayStateController;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float halfWidth = c.levelScale.levelWidth / 2f;
        float halfHeight = c.levelScale.levelHeight / 2f;

        bool mousePosInLevel = mousePos.x > -halfWidth &&
                               mousePos.x < halfWidth &&
                               mousePos.y < halfHeight &&
                               mousePos.y > -halfHeight;

        return Input.GetMouseButtonUp(0) && !InputHelper.instance.IsPointerOverUIObject() && mousePosInLevel && c.ballController.canShootAgain;
    }
}
