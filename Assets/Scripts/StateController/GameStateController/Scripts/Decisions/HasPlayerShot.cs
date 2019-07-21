using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/HasPlayerShot")]
public class HasPlayerShot : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float halfWidth = c.levelScale.levelWidth / 2f;
        float halfHeight = c.levelScale.levelHeight / 2f;

        bool mousePosInLevel = mousePos.x > -halfWidth &&
                               mousePos.x < halfWidth &&
                               mousePos.y < halfHeight &&
                               mousePos.y > -halfHeight;

        return Input.GetMouseButtonUp(0) && mousePosInLevel && !InputHelper.instance.IsPointerOverUIObject();
    }
}
