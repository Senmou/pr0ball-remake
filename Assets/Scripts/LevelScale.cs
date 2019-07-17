using UnityEngine;

public class LevelScale : MonoBehaviour {

    public float levelWidth;
    public float levelHeight;
    public float wallThickness;

    public Transform top;
    public Transform ground;
    public Transform leftWall;
    public Transform rightWall;

    private void Update() {
        top.localScale = new Vector2(levelWidth + 2f * wallThickness, wallThickness);
        ground.localScale = new Vector2(levelWidth + 2f * wallThickness, wallThickness);
        leftWall.localScale = new Vector2(wallThickness, levelHeight + 2f * wallThickness);
        rightWall.localScale = new Vector2(wallThickness, levelHeight + 2f * wallThickness);

        leftWall.position = new Vector2((-levelWidth - wallThickness) / 2f, 0f);
        rightWall.position = new Vector2((levelWidth + wallThickness) / 2f, 0f);
        top.position = new Vector2(0f, (levelHeight + wallThickness) / 2f);
        ground.position = new Vector2(0f, (-levelHeight - wallThickness) / 2f);
    }
}
