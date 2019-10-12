using System.Collections;
using UnityEngine;

/**
 * This skill creates a laser between two balls 
 **/
public class Skill_A1 : Skill {

    public float dps;
    public Material laserMat;
    public ContactFilter2D contactFilter;

    private LineRenderer line;

    private void Start() {
        coolDown = 0;
        unlockLevel = 1;

        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = laserMat;
        line.enabled = false;
    }

    protected override IEnumerator ActionCoroutine() {

        while (pending) {
            if (ballController.BallCount > 1)
                pending = false;
            yield return null;
        }

        Transform b1 = ballController.Balls[0].transform;
        Transform b2 = ballController.Balls[1].transform;

        line.enabled = true;

        RaycastHit2D[] hits = new RaycastHit2D[4];
        while (ballController.BallCount > 1) {

            line.SetPositions(new Vector3[] { b1.position, b2.position });

            Vector2 startPoint = b1.position;
            Vector2 endPoint = b2.position - b1.position;

            int numHits = Physics2D.Raycast(startPoint, endPoint, contactFilter, hits);

            for (int i = 0; i < numHits; i++) {
                hits[i].transform.GetComponent<BaseEnemy>().TakeDamage((int)(dps * Time.unscaledDeltaTime));
            }
            yield return null;
        }

        line.enabled = false;
    }
}
