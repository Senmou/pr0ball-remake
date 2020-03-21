using UnityEngine;

public class AimingLine : MonoBehaviour {

    [SerializeField] private Transform cannon;
    [SerializeField] private float reflectionLength;
    [SerializeField] private ContactFilter2D contactFilter;

    private LineRenderer line;
    private float maxDistance = 100f;

    private void Awake() {
        line = GetComponent<LineRenderer>();

        line.positionCount = 3;
        line.SetPosition(0, cannon.position);
    }

    private void Update() {

        Raycast(cannon.position, cannon.up);
    }

    private void Raycast(Vector2 rayOrigin, Vector2 direction) {

        RaycastHit2D[] hits = new RaycastHit2D[1];
        Physics2D.Raycast(rayOrigin, direction, contactFilter, hits, maxDistance);

        line.SetPosition(1, hits[0].point);

        Vector2 reflectedDirection = Vector2.Reflect(direction, hits[0].normal);

        line.SetPosition(2, reflectedDirection.normalized * reflectionLength + hits[0].point);
    }
}
